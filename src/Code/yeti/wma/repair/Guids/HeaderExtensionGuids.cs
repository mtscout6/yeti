using System;
using System.IO;

namespace yeti.wma.repair
{
    public static class HeaderExtensionGuids
    {
        public static Guid Extended_Stream_Properties = new Guid("14E6A5CB-C672-4332-8399-A96952065B5A");
        public static Guid Advanced_Mutual_Exclusion = new Guid("A08649CF-4775-4670-8A16-6E35357566CD");
        public static Guid Group_Mutual_Exclusion = new Guid("D1465A40-5A79-4338-B71B-E36B8FD6C249");
        public static Guid Stream_Prioritization = new Guid("D4FED15B-88D3-454F-81F0-ED5C45999E24");
        public static Guid Bandwidth_Sharing = new Guid("A69609E6-517B-11D2-B6AF-00C04FD908E9");
        public static Guid Language_List = new Guid("7C4346A9-EFE0-4BFC-B229-393EDE415C85");
        public static Guid Metadata = new Guid("C5F8CBEA-5BAF-4877-8467-AA8C44FA4CCA");
        public static Guid Metadata_Library = new Guid("44231C94-9498-49D1-A141-1D134E457054");
        public static Guid Index_Parameters = new Guid("D6E229DF-35DA-11D1-9034-00A0C90349BE");
        public static Guid Media_Object_Index_Parameters = new Guid("6B203BAD-3F11-48E4-ACA8-D7613DE2CFA7");
        public static Guid Timecode_Index_Parameters = new Guid("F55E496D-9797-4B5D-8C8B-604DFE9BFB24");
        public static Guid Compatibility = new Guid("26F18B5D-4584-47EC-9F5F-0E651F0452C9");
        public static Guid Advanced_Content_Encryption = new Guid("43058533-6981-49E6-9B74-AD12CB86D58C");
    }
}