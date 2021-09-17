using Java.Util.Concurrent.Atomic;

namespace CartoPrime.Droid.Models
{
    public class NotificationID
    {
        private static AtomicInteger c = new AtomicInteger(0);
        public static int getID()
        {
            return c.IncrementAndGet();
        }
    }
}