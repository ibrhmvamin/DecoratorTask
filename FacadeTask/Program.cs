namespace FacadeTask
{
    public class Program
    {

        public interface IDataSource
        {
            void WriteData(string data);
            void ReadData();

        }

        public class FileDataSource : IDataSource
        {
            private string? fileName;

            public void ReadData()
            {
                Console.WriteLine($"Reading {fileName} file");
            }

            public void WriteData(string data)
            {
                Console.WriteLine($"Writing to {fileName} file");
            }
            public FileDataSource(string fileName)
            {
                this.fileName = fileName;
            }
        }
        public class DataSourceDecorator : IDataSource
        {
            protected IDataSource? dataSource;

            public DataSourceDecorator(IDataSource dataSource) 
            {
            this.dataSource = dataSource;
            }
            public virtual void ReadData()
            => dataSource?.ReadData();

            public virtual void WriteData(string data)
                => dataSource?.WriteData(data);
        }
        public class EncryptionDecorator:DataSourceDecorator
        {
            protected IDataSource? dataSource;

            public EncryptionDecorator(IDataSource dataSource) : base(dataSource)
            {
            }
            public override void ReadData()
            => dataSource?.ReadData();

            public override void WriteData(string data)
                => dataSource?.WriteData(data);
        }
        public class CompressionDecorator : DataSourceDecorator
        {
            protected IDataSource? dataSource;

            public CompressionDecorator(IDataSource dataSource)
            :base(dataSource) 
            {
                
            }
            public void ReadData()
            => base.ReadData();

            public void WriteData(string data)
                => base.WriteData(data);
        }

        static void Main(string[] args)
        {

            IDataSource fileDataSource = new FileDataSource("example.txt");
            IDataSource encryptedFileDataSource = new EncryptionDecorator(fileDataSource);
            IDataSource compressedEncryptedFileDataSource = new CompressionDecorator(encryptedFileDataSource);

            string dataToWrite = "Hello, Decorator Pattern!";
            compressedEncryptedFileDataSource.WriteData(dataToWrite);

            compressedEncryptedFileDataSource.ReadData();
        }
    }
}