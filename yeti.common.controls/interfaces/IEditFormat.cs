using System.ComponentModel;

namespace yeti.common.controls.interfaces
{
    public interface IEditFormat : IConfigControl
    {
        [Browsable(false)]
        WaveFormat Format { get; set; }
    }
}