namespace Hardware.Driver
{
    public class Exporter
    {
        public string FullPathWithFileName
        {
            get
            {
                return string.Concat($"Zeiten{DateTime.Now.ToString("MMMM.yyyy")}.csv");
            }
        }
        public Exporter()
        {
        }

        public void Write(Recording r)
        {

            Stream f = File.Open(FullPathWithFileName, FileMode.OpenOrCreate | FileMode.Append);
            using (StreamWriter w = new StreamWriter(f))
            {
                w.WriteLine(r.ToString());
                w.Flush();
                f.Flush();
            }
            f.Close();
        }
    }
}
