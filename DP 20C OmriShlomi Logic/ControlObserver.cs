using System.Windows.Forms;

namespace DP_20C_OmriShlomi_Logic
{
    public partial class ControlObserver : IObserverControl
    {
        private Control m_controlToEnable;

        public ControlObserver(Control i_ControlToEnable)
        {
            m_controlToEnable = i_ControlToEnable;
        }

        void IObserverControl.Update()
        {
            m_controlToEnable.Enabled = true;
        }
    }
}