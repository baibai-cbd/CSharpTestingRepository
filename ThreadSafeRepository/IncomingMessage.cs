using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadSafeRepository
{
    class IncomingMessage : IDisposable
    {
        public string message { get; set; }
        
        public IncomingMessage(string message)
        {
            this.message = message;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            Console.WriteLine("message disposed...");
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing) {}
        }
    }
}
