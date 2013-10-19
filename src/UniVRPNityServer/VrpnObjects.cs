using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using Vrpn;

namespace UniVRPNity
{
    /// <summary>
    /// Contains a thread safe set of vrpn remote device.
    /// </summary>
    class VrpnObjects
    {
        /// <summary>
        /// Synchronisation object.
        /// </summary>
        private readonly object sync = new object();

        /// <summary>
        /// Monitoring the state of the remotes iterator.
        /// </summary>
        private bool valid = true;

        /// <summary>
        /// Contains all remotes informations.
        /// </summary>
        private HashSet<VrpnObjectInfo> remotes;

        /// <summary>
        /// Build an empty remotes list.
        /// </summary>
        public VrpnObjects()
        {
            remotes = new HashSet<VrpnObjectInfo>();
        }

        /// <summary>
        /// Invalidate iterator state. <see cref="Valid"/>.
        /// </summary>
        public void invalidate()
        {
            this.valid = false;
        }

        /// <summary>
        /// Validate iterator state. <see cref="Valid"/>.
        /// </summary>
        public void validate()
        {
            this.valid = true;
        }

        /// <summary>
        /// Recover iterator state.
        /// </summary>
        public bool Valid
        {
            get
            {
                return this.valid;
            }
        }

        /// <summary>
        /// Add remote object on the list. Synchronised.
        /// </summary>
        /// <param name="remote">Remote object to add.</param>
        public void SyncAdd(VrpnObjectInfo remote)
        {
            lock (sync)
            {
                remotes.Add(remote);
            }
            this.invalidate();
        }

        /// <summary>
        /// Remove a remote object on the list. Synchronised.
        /// </summary>
        /// <param name="remote">Remote object to remove.</param>s
        public void SyncRemove(VrpnObjectInfo remote)
        {
            lock (sync)
            {
                remotes.Remove(remote);
            }
            this.invalidate();
        }

        /// <summary>
        /// Call update of all remote object.
        /// </summary>
        public void SyncUpdate()
        {
            lock (sync)
            {
                foreach (VrpnObjectInfo peripheral in remotes)
                {
                    peripheral.Remote.Update();
                    if (!this.Valid)
                        break;
                }
                this.validate();
            }
        }

        /// <summary>
        /// Search a remote device.
        /// </summary>
        /// <param name="vrpnObject">Searched object.</param>
        /// <returns>True if exist, false else.</returns>
        public bool IsPresent(IVrpnObject vrpnObject)
        {
            return this[vrpnObject] != null;
        }

        /// <summary>
        /// Find a remote from it name. Iterate on all object : O(n).
        /// </summary>
        /// <param name="name">Name of the remote.</param>
        /// <returns>Found object, null else.</returns>
        public VrpnObjectInfo this[string name]
        {
            get
            {
                foreach (VrpnObjectInfo r in remotes)
                    if (r.Name == name)
                        return r;
                return null;
            }
        }

        /// <summary>
        /// Find a remote from it VrpnNet remote object. Iterate on all object : O(n).
        /// </summary>
        /// <param name="name">VrpnNet remote object.</param>
        /// <returns>Found object, null else.</returns>
        public VrpnObjectInfo this[IVrpnObject remote]
        {
            get
            {
                foreach (VrpnObjectInfo r in remotes)
                    if (r.Remote == remote)
                        return r;
                return null;
            }
        }

    }
}