namespace PetStore.utilities
{
    public class Logger
    {

        public Logger(TestContext testContext)
        {
            var fileUtil = new FileUtil();
            Context = testContext;
            LogPath = $"{fileUtil.GetBasePath()}\\Resources\\Logs\\[LOG]_{testContext.TestName}_{DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss")}.log";
        }

        public Logger(string logPath)
        {
            LogPath = logPath;
        }

        public TestContext Context { get; }
        public string LogPath { get; }


        public void Info(string message)
        {
            var filestream = File.AppendText(LogPath);
            filestream.WriteLine($"{DateTime.Now.ToString()} | INFO | {message}");
            filestream.Close();
        }

        public void Warning(string message)
        {
            var filestream = File.AppendText(LogPath);
            filestream.WriteLine($"{DateTime.Now} | WARNING | {message}");
            filestream.Close();
        }

        public void Error(Exception e)
        {
            var filestream = File.AppendText(LogPath);
            string timeStamp = DateTime.Now.ToString();

            filestream.WriteLine($"{timeStamp} | ERROR | {e.Message}");
            if (e.InnerException != null)
            {
                filestream.WriteLine("Inner Exception: " + e.InnerException.Message);
            }
            filestream.Close();
        }
    }
}
