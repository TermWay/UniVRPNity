using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace UniVRPNity
{
    /// <summary>
    /// Store events which will be dispatch to their listener in the next mainloop()
    /// </summary>
    public class BufferEvent
    {
        /// <summary>
        /// Events which will be dispatch to their listener in the next mainloop()
        /// </summary>
        private Stack<Event> waitedEvent;

        public BufferEvent()
        {
            waitedEvent = new Stack<Event>();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        /// <summary>
        /// Add an event. Thread safe.
        /// </summary>
        /// <param name="e">Event to add.</param>
        public void Push(Event e)
        {
            waitedEvent.Push(e);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        /// <summary>
        /// Return <see cref="BufferEvent.waitedEvent"/> and flush it.
        /// </summary>
        public Stack<Event> FlushEvent()
        {
            Stack<Event> waitedEventTmp = new Stack<Event>(waitedEvent);
            waitedEvent.Clear(); //Flush the stack at the end.
            return waitedEventTmp;
        }
    }
}
