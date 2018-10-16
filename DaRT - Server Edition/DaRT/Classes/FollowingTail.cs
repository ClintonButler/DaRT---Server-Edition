using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

public class FollowingTail : IDisposable
{
    private readonly Stream _fileStream;
    private readonly System.Threading.Timer _timer;

    public FollowingTail(FileInfo file,
                         Encoding encoding,
                         Action<string> fileChanged)
    {
        _fileStream = new FileStream(file.FullName,
                                     FileMode.Open,
                                     FileAccess.Read,
                                     FileShare.ReadWrite);

        _timer = new System.Threading.Timer(o => CheckForUpdate(encoding, fileChanged),
                           null,
                           0,
                           500);
    }

    private void CheckForUpdate(Encoding encoding,
                                Action<string> fileChanged)
    {
        // Read the tail of the file off
        var tail = new StringBuilder();
        int read;
        var b = new byte[1024];
        while ((read = _fileStream.Read(b, 0, b.Length)) > 0)
        {
            tail.Append(encoding.GetString(b, 0, read));
        }

        // If we have anything notify the fileChanged callback
        // If we do not, make sure we are at the end
        if (tail.Length > 0)
        {
            fileChanged(tail.ToString());
        }
        else
        {
            _fileStream.Seek(0, SeekOrigin.End);
        }
    }

    void ReleaseManagedResources()
    {
        Console.WriteLine("Releasing Managed Resources");
        if (_fileStream != null)
        {
            _fileStream.Dispose();
        }
        if (_timer != null)
        {
            _timer.Dispose();
        }
    }

    void ReleaseUnmangedResources()
    {
        Console.WriteLine("Releasing Unmanaged Resources");
    }

    public void Dispose()
    {
        Console.WriteLine("Dispose called from outside");
        // If this function is being called the user wants to release the
        // resources. lets call the Dispose which will do this for us.
        Dispose(true);

        // Now since we have done the cleanup already there is nothing left
        // for the Finalizer to do. So lets tell the GC not to call it later.
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        Console.WriteLine("Actual Dispose called with a " + disposing.ToString());
        if (disposing == true)
        {
            //someone want the deterministic release of all resources
            //Let us release all the managed resources
            ReleaseManagedResources();
        }
        else
        {
            // Do nothing, no one asked a dispose, the object went out of
            // scope and finalized is called so lets next round of GC 
            // release these resources
        }

        // Release the unmanaged resource in any case as they will not be 
        // released by GC
        ReleaseUnmangedResources();

    }

    ~FollowingTail()
    {
        Console.WriteLine("Finalizer called");
        // The object went out of scope and finalized is called
        // Lets call dispose in to release unmanaged resources 
        // the managed resources will anyways be released when GC 
        // runs the next time.
        Dispose(false);
    }
}