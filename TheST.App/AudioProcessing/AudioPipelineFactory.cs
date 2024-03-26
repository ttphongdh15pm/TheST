namespace TheST.App.AudioProcessing
{
    internal static class AudioPipelineFactory
    {
        public static IAudioPipeline Create(IAudioBufferDataHandler handler, bool applyEffect)
        {
            if (applyEffect)
            {
                return new PythonProcessAudioPipeline(handler);
            }
            return new EmptyAudioPipeline(handler);
        }
    }
}
