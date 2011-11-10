using System;

namespace yeti.wma.enums
{
    [Flags]
    public enum WMT_RIGHTS : uint
    {
        /// <summary>
        /// This rigth is not defined in the WMF SDK, I added it to
        /// play files with no DRM
        /// </summary>
        WMT_RIGHT_NO_DRM = 0x00000000,

        WMT_RIGHT_PLAYBACK = 0x00000001,

        WMT_RIGHT_COPY_TO_NON_SDMI_DEVICE = 0x00000002,

        WMT_RIGHT_COPY_TO_CD = 0x00000008,

        WMT_RIGHT_COPY_TO_SDMI_DEVICE = 0x00000010,

        WMT_RIGHT_ONE_TIME = 0x00000020,

        WMT_RIGHT_SAVE_STREAM_PROTECTED = 0x00000040,

        WMT_RIGHT_SDMI_TRIGGER = 0x00010000,

        WMT_RIGHT_SDMI_NOMORECOPIES = 0x00020000
    }
}