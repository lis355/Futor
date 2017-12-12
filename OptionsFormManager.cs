namespace Futor
{
    public class OptionsFormManager
    {
        readonly OptionsForm _form;
        readonly AudioManager _audioManager;

        public OptionsFormManager(OptionsForm form)
        {
            _form = form;

            _audioManager = new AudioManager();
            _audioManager.Init();
        }

        public void OnFormLoad()
        {
            _audioManager.Start();
        }

        public void OnFormClosed()
        {
            _audioManager.Finish();
        }
    }
}
