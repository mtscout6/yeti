using System.ComponentModel;

namespace yeti.common.controls.interfaces
{
    public interface IEditAudioWriterConfig : IConfigControl
    {
        /// <summary>
        /// Implementors must set [Browsable(false)] attribute to this property
        /// </summary>
        [Browsable(false)]
        AudioWriterConfig Config { get; set; }
    }
}