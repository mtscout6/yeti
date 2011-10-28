using System;
using yeti.wma.enums;

namespace yeti.wma.structs
{
    /// <summary>
    /// Represent the WMF attributes that can be present in a header of an ASF stream.
    /// There are defined explicit convertion operator for every possible data type 
    /// represented with this structure. 
    /// </summary>
    public struct WM_Attr
    {
        private WMT_ATTR_DATATYPE m_DataType;
        private object m_Value;
        private string m_Name;

        /// <summary>
        /// Initialize the WM_Attr streucture with proper values.
        /// </summary>
        /// <param name="name">Name of the attribute</param>
        /// <param name="type">WMT_ATTR_DATATYPE enum describing the type of the attribute. </param>
        /// <param name="val">The atrtibute value. This param is an obcjet and must match the 
        /// param type, ex. If type is WMT_ATTR_DATATYPE.WMT_TYPE_BOOL val param must be a valid <code>bool</code> and so on. </param>
        public WM_Attr(string name, WMT_ATTR_DATATYPE type, object val)
        {
            m_Name = name;
            this.m_DataType = type;
            switch (type)
            {
                case WMT_ATTR_DATATYPE.WMT_TYPE_BINARY:
                    m_Value = (byte[])val;
                    break;
                case WMT_ATTR_DATATYPE.WMT_TYPE_BOOL:
                    m_Value = (bool)val;
                    break;
                case WMT_ATTR_DATATYPE.WMT_TYPE_DWORD:
                    m_Value = (uint)val;
                    break;
                case WMT_ATTR_DATATYPE.WMT_TYPE_GUID:
                    m_Value = (Guid)val;
                    break;
                case WMT_ATTR_DATATYPE.WMT_TYPE_QWORD:
                    m_Value = (ulong)val;
                    break;
                case WMT_ATTR_DATATYPE.WMT_TYPE_STRING:
                    m_Value = (string)val;
                    break;
                case WMT_ATTR_DATATYPE.WMT_TYPE_WORD:
                    m_Value = (ushort)val;
                    break;
                default:
                    throw new ArgumentException("Invalid data type", "type");
            }
        }

        /// <summary>
        /// ToString is overrided to provide the string representation in "name=value" format.
        /// </summary>
        /// <returns>String represantation of this struct in "name=value" format.</returns>
        public override string ToString()
        {
            return string.Format("{0} = {1}", m_Name, m_Value);
        }

        /// <summary>
        /// Name of the attribute
        /// </summary>
        public string Name
        {
            get { return m_Name; }
        }

        /// <summary>
        /// Data type of the attribute
        /// </summary>
        public WMT_ATTR_DATATYPE DataType
        {
            get { return m_DataType; }
        }

        /// <summary>
        /// Read/Write object representing the value of the attribute. 
        /// When writing this property the object must match to the DataType
        /// </summary>
        public object Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                switch (m_DataType)
                {
                    case WMT_ATTR_DATATYPE.WMT_TYPE_BOOL:
                        m_Value = (bool)value;
                        break;
                    case WMT_ATTR_DATATYPE.WMT_TYPE_DWORD:
                        m_Value = (uint)value;
                        break;
                    case WMT_ATTR_DATATYPE.WMT_TYPE_WORD:
                        m_Value = (ushort)value;
                        break;
                    case WMT_ATTR_DATATYPE.WMT_TYPE_QWORD:
                        m_Value = (ulong)value;
                        break;
                    case WMT_ATTR_DATATYPE.WMT_TYPE_GUID:
                        m_Value = (Guid)value;
                        break;
                    case WMT_ATTR_DATATYPE.WMT_TYPE_STRING:
                        m_Value = (string)value;
                        break;
                    case WMT_ATTR_DATATYPE.WMT_TYPE_BINARY:
                        // Try Binary values as byte array only
                        m_Value = (byte[])value;
                        break;
                };
            }
        }

        #region Type convertion operators

        public static explicit operator string(WM_Attr attr)
        {
            if (attr.m_DataType == WMT_ATTR_DATATYPE.WMT_TYPE_STRING)
            {
                return (string)attr.m_Value;
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public static explicit operator bool(WM_Attr attr)
        {
            if (attr.m_DataType == WMT_ATTR_DATATYPE.WMT_TYPE_BOOL)
            {
                return (bool)attr.m_Value;
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public static explicit operator Guid(WM_Attr attr)
        {
            if (attr.m_DataType == WMT_ATTR_DATATYPE.WMT_TYPE_GUID)
            {
                return (Guid)attr.m_Value;
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public static explicit operator byte[](WM_Attr attr)
        {
            if (attr.m_DataType == WMT_ATTR_DATATYPE.WMT_TYPE_BINARY)
            {
                return (byte[])attr.m_Value;
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public static explicit operator ulong(WM_Attr attr)
        {
            switch (attr.m_DataType)
            {
                case WMT_ATTR_DATATYPE.WMT_TYPE_DWORD:
                case WMT_ATTR_DATATYPE.WMT_TYPE_QWORD:
                case WMT_ATTR_DATATYPE.WMT_TYPE_WORD:
                    return (ulong)attr.m_Value;
                default:
                    throw new InvalidCastException();
            }
        }

        public static explicit operator long(WM_Attr attr)
        {
            return (long)(ulong)attr;
        }

        public static explicit operator int(WM_Attr attr)
        {
            return (int)(ulong)attr;
        }

        public static explicit operator uint(WM_Attr attr)
        {
            return (uint)(ulong)attr;
        }

        public static explicit operator ushort(WM_Attr attr)
        {
            return (ushort)(ulong)attr;
        }
        #endregion
    }
}