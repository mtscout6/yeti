//  Widows Media Format Utils classes
//
//  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
//  PURPOSE. IT CAN BE DISTRIBUTED FREE OF CHARGE AS LONG AS THIS HEADER 
//  REMAINS UNCHANGED.
//
//  Email:  yetiicb@hotmail.com
//
//  Copyright (C) 2002-2004 Idael Cardoso. 
//

using System;
using System.Text;
using System.Runtime.InteropServices;
using yeti.wma.enums;
using yeti.wma.interfaces;
using yeti.wma.structs;

namespace yeti.wma.internals
{
    /// <summary>
    /// Helper class to encapsulate IWMHeaderInfo interface
    /// </summary>
    internal class WMHeaderInfo
    {
        private IWMHeaderInfo m_HeaderInfo;

        /// <summary>
        /// WMHeaderInfo constructor
        /// </summary>
        /// <param name="headinfo">IWMHeaderInfo to wrap</param>
        public WMHeaderInfo(IWMHeaderInfo headinfo)
        {
            m_HeaderInfo = headinfo;
        }

        /// <summary>
        /// Internal IWMHeaderInfo
        /// </summary>
        public IWMHeaderInfo HeaderInfo
        {
            get { return m_HeaderInfo; }
        }

        /// <summary>
        /// Add a marher to the header info. Wraps IWMHeaderInfo.AddMarker
        /// </summary>
        /// <param name="m">Marker to add. <see cref="Marker"/></param>
        public void AddMarker(Marker m)
        {
            m_HeaderInfo.AddMarker(m.Name, m.Time);
        }

        /// <summary>
        /// Get a marker. Wraps IWMHeaderInfo.GetMarker
        /// </summary>
        /// <param name="index">Index of the desired marker</param>
        /// <returns>The desired marker. <see cref="Marker"/></returns>
        public Marker GetMarker(int index)
        {
            ulong time;
            ushort namelen = 0;
            StringBuilder name;
            m_HeaderInfo.GetMarker((ushort)index, null, ref namelen, out time);
            name = new StringBuilder(namelen);
            m_HeaderInfo.GetMarker((ushort)index, name, ref namelen, out time);
            return new Marker(name.ToString(), time);
        }

        /// <summary>
        /// Remove a marker. Wraps IWMHeaderInfo.RemoveMarker
        /// </summary>
        /// <param name="index">Index of the marker to remove</param>
        public void RemoveMarker(int index)
        {
            m_HeaderInfo.RemoveMarker((ushort)index);
        }

        /// <summary>
        /// Add a scrip. Wraps IWMHeaderInfo.AddScript
        /// </summary>
        /// <param name="s">Scrip to add. <see cref="Script"/></param>
        public void AddScript(Script s)
        {
            m_HeaderInfo.AddScript(s.Type, s.Command, s.Time);
        }

        /// <summary>
        /// Get a script from the header info. Wraps IWMHeaderInfo.GetScript
        /// </summary>
        /// <param name="index">Index of desired script</param>
        /// <returns>Desired script. <see cref="Script"/></returns>
        public Script GetScript(int index)
        {
            ushort commandlen = 0, typelen = 0;
            ulong time;
            StringBuilder command, type;
            m_HeaderInfo.GetScript((ushort)index, null, ref typelen, null, ref commandlen, out time);
            command = new StringBuilder(commandlen);
            type = new StringBuilder(typelen);
            m_HeaderInfo.GetScript((ushort)index, type, ref typelen, command, ref commandlen, out time);
            return new Script(type.ToString(), command.ToString(), time);
        }

        /// <summary>
        /// Remove a script. Wraps IWMHeaderInfo.RemoveScript
        /// </summary>
        /// <param name="index">Index of script to remove</param>
        public void RemoveScript(int index)
        {
            m_HeaderInfo.RemoveScript((ushort)index);
        }

        /// <summary>
        /// Number of scripts in header info object. Wraps IWMHeaderInfo.GetScriptCount
        /// </summary>
        public int ScriptCount
        {
            get
            {
                ushort res;
                m_HeaderInfo.GetScriptCount(out res);
                return res;
            }
        }

        /// <summary>
        /// Number of markers in the header info object. Wraps IWMHeaderInfo.GetMarkerCount
        /// </summary>
        public int MarkerCount
        {
            get
            {
                ushort res;
                m_HeaderInfo.GetMarkerCount(out res);
                return res;
            }
        }

        /// <summary>
        /// Number of markers in the header info object for the specified stream. Wraps IWMHeaderInfo.GetAttributeCount
        /// </summary>
        /// <param name="StreamNumber">Stream number. Zero means file level attributes</param>
        /// <returns>Number of attributes</returns>
        public int AttributeCount(int StreamNumber)
        {
            ushort res;
            m_HeaderInfo.GetAttributeCount((ushort)StreamNumber, out res);
            return res;
        }

        /// <summary>
        /// File level attribute count.
        /// </summary>
        /// <returns>File level attribute count</returns>
        public int AttributeCount()
        {
            return AttributeCount(0);
        }

        /// <summary>
        /// Get the attribute by index of specific stream. Wraps Number of markers in the header info object. Wraps IWMHeaderInfo.GetAttibuteByIndex
        /// </summary>
        /// <param name="StreamNumber">Stream number. Zero return a file level atrtibute</param>
        /// <param name="index">Attribute index</param>
        /// <returns>Desired attribute <see cref="WM_Attr"/></returns>
        public WM_Attr GetAttribute(int StreamNumber, int index)
        {
            WMT_ATTR_DATATYPE type;
            StringBuilder name;
            object obj;
            ushort stream = (ushort)StreamNumber;
            ushort namelen = 0;
            ushort datalen = 0;
            m_HeaderInfo.GetAttributeByIndex((ushort)index, ref stream, null, ref namelen, out type, IntPtr.Zero, ref datalen);
            name = new StringBuilder(namelen);
            switch (type)
            {
                case WMT_ATTR_DATATYPE.WMT_TYPE_BOOL:
                case WMT_ATTR_DATATYPE.WMT_TYPE_DWORD:
                    obj = (uint)0;
                    break;
                case WMT_ATTR_DATATYPE.WMT_TYPE_GUID:
                    obj = Guid.NewGuid();
                    break;
                case WMT_ATTR_DATATYPE.WMT_TYPE_QWORD:
                    obj = (ulong)0;
                    break;
                case WMT_ATTR_DATATYPE.WMT_TYPE_WORD:
                    obj = (ushort)0;
                    break;
                case WMT_ATTR_DATATYPE.WMT_TYPE_STRING:
                case WMT_ATTR_DATATYPE.WMT_TYPE_BINARY:
                    obj = new byte[datalen];
                    break;
                default:
                    throw new InvalidOperationException(string.Format("Not supported data type: {0}", type.ToString()));
            }
            GCHandle h = GCHandle.Alloc(obj, GCHandleType.Pinned);
            try
            {
                IntPtr ptr = h.AddrOfPinnedObject();
                m_HeaderInfo.GetAttributeByIndex((ushort)index, ref stream, name, ref namelen, out type, ptr, ref datalen);
                switch (type)
                {
                    case WMT_ATTR_DATATYPE.WMT_TYPE_STRING:
                        obj = Marshal.PtrToStringUni(ptr);
                        break;
                    case WMT_ATTR_DATATYPE.WMT_TYPE_BOOL:
                        obj = ((uint)obj != 0);
                        break;
                }
            }
            finally
            {
                h.Free();
            }
            return new WM_Attr(name.ToString(), type, obj);
        }

        /// <summary>
        /// Retrun the file level attribute by index.
        /// </summary>
        /// <param name="index">Index of desired attribute</param>
        /// <returns><see cref="WM_Attr"/></returns>
        public WM_Attr GetAttribute(int index)
        {
            return GetAttribute(0, index);
        }

        /// <summary>
        /// Get the header attribute by name and by stream number. Wraps IWMHeaderInfo.GetAttributeByName
        /// </summary>
        /// <param name="StreamNumber">Stream numer. Zero means file level attributes</param>
        /// <param name="name">Nma of the desired attribute</param>
        /// <returns>Desired attribute <see cref="WM_Attr"/></returns>
        public WM_Attr GetAttribute(int StreamNumber, string name)
        {
            ushort stream = (ushort)StreamNumber;
            ushort datalen = 0;
            object obj;
            WMT_ATTR_DATATYPE type;

            m_HeaderInfo.GetAttributeByName(ref stream, name, out type, IntPtr.Zero, ref datalen);
            switch (type)
            {
                case WMT_ATTR_DATATYPE.WMT_TYPE_BOOL:
                case WMT_ATTR_DATATYPE.WMT_TYPE_DWORD:
                    obj = (uint)0;
                    break;
                case WMT_ATTR_DATATYPE.WMT_TYPE_GUID:
                    obj = Guid.NewGuid();
                    break;
                case WMT_ATTR_DATATYPE.WMT_TYPE_QWORD:
                    obj = (ulong)0;
                    break;
                case WMT_ATTR_DATATYPE.WMT_TYPE_WORD:
                    obj = (ushort)0;
                    break;
                case WMT_ATTR_DATATYPE.WMT_TYPE_STRING:
                case WMT_ATTR_DATATYPE.WMT_TYPE_BINARY:
                    obj = new byte[datalen];
                    break;
                default:
                    throw new InvalidOperationException(string.Format("Not supported data type: {0}", type.ToString()));
            }
            GCHandle h = GCHandle.Alloc(obj, GCHandleType.Pinned);
            try
            {
                IntPtr ptr = h.AddrOfPinnedObject();
                m_HeaderInfo.GetAttributeByName(ref stream, name, out type, ptr, ref datalen);
                switch (type)
                {
                    case WMT_ATTR_DATATYPE.WMT_TYPE_STRING:
                        obj = Marshal.PtrToStringUni(ptr);
                        break;
                    case WMT_ATTR_DATATYPE.WMT_TYPE_BOOL:
                        obj = ((uint)obj != 0);
                        break;
                }
            }
            finally
            {
                h.Free();
            }
            return new WM_Attr(name, type, obj);
        }

        /// <summary>
        /// Return a file level attribute by name.
        /// </summary>
        /// <param name="name">Name of desired attribute</param>
        /// <returns>Desired attribute <see cref="WM_Attr"/></returns>
        public WM_Attr GetAttribute(string name)
        {
            return GetAttribute(0, name);
        }

        /// <summary>
        /// Set an attribute specifying a stream number. Wraps IWMHeaderInfo.SetAttribute
        /// </summary>
        /// <param name="StreamNumber">Stream number. Zero for file level attributes.</param>
        /// <param name="attr">Attribute to set. <see cref="WM_Attr"/></param>
        public void SetAttribute(int StreamNumber, WM_Attr attr)
        {
            object obj;
            ushort size;
            switch (attr.DataType)
            {
                case WMT_ATTR_DATATYPE.WMT_TYPE_STRING:
                    byte[] arr = Encoding.Unicode.GetBytes((string)attr.Value + (char)0);
                    obj = arr;
                    size = (ushort)arr.Length;
                    break;
                case WMT_ATTR_DATATYPE.WMT_TYPE_BOOL:
                    obj = (uint)((bool)attr ? 1 : 0);
                    size = 4;
                    break;
                case WMT_ATTR_DATATYPE.WMT_TYPE_BINARY:
                    obj = (byte[])attr.Value;
                    size = (ushort)((byte[])obj).Length;
                    break;
                case WMT_ATTR_DATATYPE.WMT_TYPE_DWORD:
                    obj = (uint)attr;
                    size = 4;
                    break;
                case WMT_ATTR_DATATYPE.WMT_TYPE_QWORD:
                    obj = (ulong)attr;
                    size = 8;
                    break;
                case WMT_ATTR_DATATYPE.WMT_TYPE_WORD:
                    obj = (ushort)attr;
                    size = 2;
                    break;
                case WMT_ATTR_DATATYPE.WMT_TYPE_GUID:
                    obj = (Guid)attr;
                    size = (ushort)Marshal.SizeOf(typeof(Guid));
                    break;
                default:
                    throw new ArgumentException("Invalid data type", "attr");
            }
            GCHandle h = GCHandle.Alloc(obj, GCHandleType.Pinned);
            try
            {
                m_HeaderInfo.SetAttribute((ushort)StreamNumber, attr.Name, attr.DataType, h.AddrOfPinnedObject(), size);
            }
            finally
            {
                h.Free();
            }
        }

        /// <summary>
        /// Set file level attributes
        /// </summary>
        /// <param name="attr">Attribute to set <see cref="WM_Attr"/></param>
        public void SetAttribute(WM_Attr attr)
        {
            SetAttribute(0, attr);
        }

        /// <summary>
        /// Read only. File level attributes indexed by integer index. <see cref="WM_Attr"/>
        /// </summary>
        [System.Runtime.CompilerServices.IndexerName("Attributes")]
        public WM_Attr this[int index]
        {
            get
            {
                return GetAttribute(index);
            }
        }

        /// <summary>
        /// Read/Write. File level attributes indexed by name. <see cref="WM_Attr"/>
        /// </summary>
        [System.Runtime.CompilerServices.IndexerName("Attributes")]
        public WM_Attr this[string name]
        {
            get
            {
                return GetAttribute(name);
            }
            set
            {
                SetAttribute(value);
            }
        }
    }
}
