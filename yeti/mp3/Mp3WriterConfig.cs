//
//  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
//  PURPOSE. IT CAN BE DISTRIBUTED FREE OF CHARGE AS LONG AS THIS HEADER 
//  REMAINS UNCHANGED.
//
//  Email:  yetiicb@hotmail.com
//
//  Copyright (C) 2002-2003 Idael Cardoso. 
//  
//  LAME ( LAME Ain't an Mp3 Encoder ) 
//  You must call the fucntion "beVersion" to obtain information  like version 
//  numbers (both of the DLL and encoding engine), release date and URL for 
//  lame_enc's homepage. All this information should be made available to the 
//  user of your product through a dialog box or something similar.
//  You must see all information about LAME project and legal license infos at 
//  http://www.mp3dev.org/  The official LAME site
//
//
//  About Thomson and/or Fraunhofer patents:
//  Any use of this product does not convey a license under the relevant 
//  intellectual property of Thomson and/or Fraunhofer Gesellschaft nor imply 
//  any right to use this product in any finished end user or ready-to-use final 
//  product. An independent license for such use is required. 
//  For details, please visit http://www.mp3licensing.com.
//

using System;
using System.Runtime.Serialization;
using yeti.mp3.configuration;

namespace yeti.mp3
{
    /// <summary>
    /// Config information for MP3 writer
    /// </summary>
    [Serializable]
    public class Mp3WriterConfig : AudioWriterConfig
    {
        private BE_CONFIG m_BeConfig;

        protected Mp3WriterConfig(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            m_BeConfig = (BE_CONFIG)info.GetValue("BE_CONFIG", typeof(BE_CONFIG));
        }

        public Mp3WriterConfig(WaveFormat InFormat, BE_CONFIG beconfig)
            : base(InFormat)
        {
            m_BeConfig = beconfig;
        }

        public Mp3WriterConfig(WaveFormat InFormat)
            : this(InFormat, new BE_CONFIG(InFormat))
        {
        }

        public Mp3WriterConfig()
            : this(new WaveFormat(44100, 16, 2))
        {
        }

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("BE_CONFIG", m_BeConfig, m_BeConfig.GetType());
        }

        public BE_CONFIG Mp3Config
        {
            get
            {
                return m_BeConfig;
            }
            set
            {
                m_BeConfig = value;
            }
        }
    }
}
