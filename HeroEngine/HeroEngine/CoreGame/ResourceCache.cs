using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HeroEngine.Operations;
namespace HeroEngine.CoreGame
{
    /// <summary>
    /// A huge database of content used for holding textures, sounds and other stuff in a easily usable method set.
    /// </summary>
    /// <typeparam name="obj_type">The type of resource to hold.</typeparam>
    class ResourceCache<obj_type>
    {
        List<obj_type> resources;
        List<string> names = new List<string>();
        Exception AlreadyPresent = new Exception("The resource you are trying to add conflicts with another.");
        Exception NotPresent = new Exception("The resource you are trying to get does not exist.");
        public ResourceCache()
        {
            resources = new List<obj_type>();
        }
        /// <summary>
        /// Adds a resource to the cache.
        /// </summary>
        /// <param name="resource">Type of resource.</param>
        /// <param name="name">Name to use.</param>
        public void AddResource(obj_type resource, string name)
        {
            if (names.Contains(name))
            {
                //throw AlreadyPresent;
                int i = names.IndexOf(name);
                resources[i] = (resource);
                System.Diagnostics.Debug.WriteLine("Overwriting " + name);
            }
            else
            {
                names.Add(name);
                resources.Add(resource);
                System.Diagnostics.Debug.WriteLine("Adding " + name);
            }
        }

        public obj_type GetResource(string name)
        {
           obj_type obj = resources[ArrayFunc.GetIndexOfObject(names.ToArray(), name)];
           
           if (obj.ToString() == "-1")
           {
               throw NotPresent;
           }

           return obj;
        }

        public obj_type GetResourceByIndex(int i)
        {
            return resources[i];
        }

        public int CountResources() { return names.Count; }
    }
}
