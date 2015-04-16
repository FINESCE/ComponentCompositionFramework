using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Insero.ComponentCompositionFramework.Components.NgsiProducer.Internal
{
   internal sealed class Accumulator<TItem> : IDisposable
   {
      public event EventHandler<AccumulatedEventArgs<TItem>> Accumulated;

      private TimeSpan _interval;
      private Timer _timer;
      private List<TItem> _items = new List<TItem>();
      private object _lock = new object();

      internal Accumulator( TimeSpan interval )
      {
         _timer = new Timer( new TimerCallback( Timer_Elapsed ), null, (int)interval.TotalMilliseconds, Timeout.Infinite );
         _interval = interval;
      }

      private void Timer_Elapsed( object state )
      {
         if ( _timer != null )
         {
            try
            {
               List<TItem> items;
               lock ( _lock )
               {
                  items = _items;
                  _items = new List<TItem>();
               }
               RaiseAccumulated( items );
            }
            finally
            {
               lock ( _lock )
               {
                  if ( _timer != null )
                  {
                     _timer.Change( _interval, Timeout.InfiniteTimeSpan );
                  }
               }
            }
         }
      }

      internal void Accumulate( IEnumerable<TItem> items )
      {
         lock ( _lock )
         {
            _items.AddRange( items );
         }
      }

      internal void Accumulate( TItem item )
      {
         lock ( _lock )
         {
            _items.Add( item );
         }
      }

      private void RaiseAccumulated( List<TItem> accumulation )
      {
         if ( Accumulated != null )
         {
            Accumulated( this, new AccumulatedEventArgs<TItem>( accumulation ) );
         }
      }

      #region IDisposable Members

      public void Dispose()
      {
         lock ( _lock )
         {
            if ( _timer != null )
            {
               _timer.Dispose();
               _timer = null;
            }
         }
      }

      #endregion
   }
}
