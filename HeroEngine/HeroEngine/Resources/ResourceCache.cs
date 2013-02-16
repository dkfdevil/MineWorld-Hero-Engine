using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using HeroEngineShared;
using System.IO;

namespace HeroEngine.CoreGame
{
    /// <summary>
    /// A huge database of content used for holding textures, sounds and other stuff in a easily usable method set.
    /// </summary>
    /// <typeparam name="obj_type">The type of resource to hold.</typeparam>
    public class ResourceCache<obj_type>
    {
        Dictionary<string,obj_type> resources;

        Exception AlreadyPresent = new Exception("The resource you are trying to add conflicts with another.");
        Exception NotPresent = new Exception("The resource you are trying to get does not exist.");
        Exception AlreadyRemoved = new Exception("The resource you are trying to remove isnt present");

        public ResourceCache(ContentManager man)
        {
            resources = new Dictionary<string, obj_type>();
        }

        /// <summary>
        /// Adds a resource to the cache.
        /// </summary>
        /// <param name="resource">Type of resource.</param>
        /// <param name="name">Name to use.</param>
        public void AddResource(obj_type resource, string name)
        {
            if(resources.Keys.Contains(name))
            {
                //throw AlreadyPresent;
                resources[name] = resource;
                System.Diagnostics.Debug.WriteLine("Overwriting " + name);
            }
            else
            {
                resources.Add(name, resource);
                System.Diagnostics.Debug.WriteLine("Adding " + name);
            }
        }

        /// <summary>
        /// Removes a resource from the cache.
        /// </summary>
        /// <param name="resource">Type of resource.</param>
        /// <param name="name">Name to use.</param>
        public void RemoveResource(string name)
        {
            if (resources.Keys.Contains(name))
            {
                resources.Remove(name);
                System.Diagnostics.Debug.WriteLine("Removing " + name);
            }
            else
            {
                throw AlreadyRemoved;
            }
        }

        public obj_type GetResource(string name)
        {
            obj_type obj;
            bool succes = resources.TryGetValue(name, out obj);

            if (!succes)
            {
                throw NotPresent;
            }

            return obj;
        }

        public int CountResources() 
        {
            return resources.Count();
        }
    }
}
