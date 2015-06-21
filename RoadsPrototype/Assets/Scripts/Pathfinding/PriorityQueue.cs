/*
  PriorityQueue.cs
  RoadsPrototype
  Created by Rohun Banerji on June 07, 2015.
  Copyright (c) 2015 Rohun Banerji. All rights reserved.
*/

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding
{
    /// <summary>
    /// Implemented as a sortedDictionary of queues. If multiple items have the same priority they are queued under the same element.
    /// </summary>
    /// <typeparam name="P"></typeparam>
    /// <typeparam name="T"></typeparam>
    public class PriorityQueue<P, T> : IEnumerable
    {
        private SortedDictionary<P, Queue<T>> dictionary = new SortedDictionary<P, Queue<T>>();

        /// <summary>
        /// Adds the value with the provided priority. If other values are present with the same priority, the provided value is enqueue behind the older values.
        /// </summary>
        public void Enqueue(P priority, T value)
        {
            Queue<T> queue;
            //check if the priority already exists in the dictionary
            if(!dictionary.TryGetValue(priority, out queue))
            {
                //create a new queue if nothing exists with the same priority
                queue = new Queue<T>();
                dictionary.Add(priority, queue);
            }
            //add the value to the queue
            queue.Enqueue(value);
        }

        public T Dequeue()
        {
            //first queue in the dictionary
            var firstPair = dictionary.First();
            //get the value from the queue
            var value = firstPair.Value.Dequeue();
            //remove the queue if no more values are present
            if(firstPair.Value.Count==0)
            {
                dictionary.Remove(firstPair.Key);
            }
            return value;
        }

        public bool IsEmpty
        {
            get { return !dictionary.Any(); }
        }

        public void Clear()
        {
            dictionary.Clear();
        }

        //IEnumerator implementation
        public IEnumerator GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }
    }
}
