namespace Futor
{
    public class OptionsFormManager
    {
        readonly OptionsForm _form;

        AudioManager _audioManager;

        public OptionsFormManager(OptionsForm form)
        {
            _form = form;

            _audioManager = new AudioManager();
            _audioManager.Init();
        }
    }
}
