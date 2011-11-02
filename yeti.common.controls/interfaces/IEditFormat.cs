using System.ComponentModel;
using yeti.common.controls.interfaces;

namespace yeti.common.controls.interfaces
{
    public interface IEditFormat : IConfigControl
    {
        [Browsable(false)]
        WaveFormat Format { get; set; }
    }
}