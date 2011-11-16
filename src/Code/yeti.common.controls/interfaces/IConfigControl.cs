using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace yeti.common.controls.interfaces
{
    public interface IConfigControl
    {
        void DoApply();
        void DoSetInitialValues();
        /// <summary>
        /// Implementors must set [Browsable(false)] attribute to this property
        /// </summary>
        [Browsable(false)]
        Control ConfigControl { get; }
        /// <summary>
        /// Implementors must set [Browsable(false)] attribute to this property
        /// </summary>
        [Browsable(false)]
        string ControlName { get; }
        event EventHandler ConfigChange;
    }
}