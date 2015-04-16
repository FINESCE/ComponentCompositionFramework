/*
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insero.ComponentCompositionFramework.Components.Utilities
{

   /// <summary>
   /// Class implementing the Disposable pattern of IDesign.
   /// </summary>
   public class ExtendedDisposableBase : IExtendedDisposable
   {
      protected object _syncRoot;

      /// <summary>
      /// Creates a new ExtendedDisposableBase instance.
      /// </summary>
      public ExtendedDisposableBase()
      {
         _syncRoot = new object();
      }

      #region IDisposable Members

      /// <summary>
      /// Destructor used for implementing the Disposable pattern of IDesign.
      /// Cleans up and disposes unmanaged resources if necessary.
      /// </summary>
      ~ExtendedDisposableBase()
      {
         Dispose( false );
      }

      /// <summary>
      /// IDisposable clean-up method. Frees any resources.
      /// </summary>
      protected virtual void Dispose( bool disposing )
      {
         /* TODO: Implement clean-up code here! */
      }

      /// <summary>
      /// Gets whether the object is disposed.
      /// </summary>
      protected bool IsDisposed
      {
         get
         {
            lock ( _syncRoot )
            {
               return m_Disposed;
            }
         }
      }
      bool m_Disposed = false;

      /// <summary>
      /// Method implementing main cleanup required when implementing IDisposable.
      /// Always remember to invoke this method explicitly or implicitly when cleaning up the object!
      /// </summary>
      /// <example>
      /// Explicit disposing of an object is merely done by
      /// <code>
      /// ExtendedDisposableBase myObject = new ExtendedDisposableBase();
      /// ...
      /// /*When you are done */
      /// myObject.Dispose();
      /// Debug.Assert( myObject.Disposed == true );
      /// ...
      /// // Note: Never use objects are they are disposed!
      /// ...
      /// myObject.MyMethod(); // &lt;-- Throws ObjectDisposedException
      /// </code>
      /// </example>
      /// <example>
      /// An example of implicit disposing of an object is
      /// <code>
      /// using( ExtendedDisposableBase myObject = new ExtendedDisposableBase() as IDisposable )
      /// {
      ///     myObject.MyMethod();
      ///     ...
      ///     /* When you are done */
      /// }
      /// Debug.Assert( myObject.Disposed == true );
      /// </code>
      /// The end of the using-scope implicitly invokes IDisposable.Dispose().
      /// </example>
      public void Dispose()
      {
         lock ( _syncRoot )
         {
            if ( m_Disposed == false )
            {
               Dispose( true );
               GC.SuppressFinalize( this );
               m_Disposed = true;
               RaiseDisposed();
            }
         }
      }

      #endregion

      /// <summary>
      /// Throws an ObjectDisposedException if the object has been disposed.
      /// </summary>
      protected void ThrowIfDisposed()
      {
         lock ( _syncRoot )
         {
            if ( IsDisposed ) // <-- Verify this in every method
            {
               throw new ObjectDisposedException( "Object is disposed" );
            }
         }
      }

      #region IExtendedDisposable Members

      public event EventHandler Disposed;

      private void RaiseDisposed()
      {
         var handler = Disposed;
         if ( handler != null )
         {
            handler( this, EventArgs.Empty );
         }
      }

      #endregion
   }
}
