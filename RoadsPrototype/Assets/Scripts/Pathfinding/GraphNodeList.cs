/*
  GraphNodeList.cs
  RoadsPrototype
  Created by Rohun Banerji on June 07, 2015.
  Copyright (c) 2015 Rohun Banerji. All rights reserved.
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding
{
    /// <summary>
    /// The node list is implemented as a dictionary, with the ID for the node being hashed for quick access
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GraphNodeList<T> : IEnumerable
    {
        //Private members
        private Dictionary<int, GraphNode<T>> nodeTable = new Dictionary<int, GraphNode<T>>();

        //Public Methods

        /// <summary>
        /// Allows access via the [] operator like an array/list
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public GraphNode<T> this[int ID]
        {
            get
            {
                return nodeTable[ID];
            }
        }

        /// <summary>
        /// Fills the node parameter with the value if found and return true
        /// </summary>
        public bool TryGetValue(int ID, out GraphNode<T> nodeToReturn)
        {
            bool containsNode = ContainsNodeWithID(ID);
            nodeToReturn = containsNode ? this[ID] : null;
            return containsNode;
        }

        /// <summary>
        /// Add a new node
        /// </summary>
        /// <param name="node"></param>
        public void Add(int ID, GraphNode<T> node)
        {
            nodeTable.Add(ID, node);
        }

        /// <summary>
        /// Remove a node - slower than RemoveByID
        /// </summary>
        /// <param name="node"></param>
        //public bool RemoveNode(GraphNode<T> node)
        //{
        //    return nodeTable.Remove(node.ID);
        //}

        public bool ContainsNodeWithID(int ID)
        {
            return nodeTable.ContainsKey(ID);
        }
        
        public bool ContainsNodeWithValue(T value)
        {
            return nodeTable.Values.Any(node => node.Value.Equals(value));
        }
        
        public bool ContainsNode(GraphNode<T> node)
        {
            return nodeTable.ContainsValue(node);
        }

        public void Clear()
        {
            nodeTable.Clear();
        }

        /// <summary>
        /// Returns an IEnumerator so the NodeList can be iterated over
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return nodeTable.Values.GetEnumerator();
            //return new NodeListEnumerator(nodeTable;.GetEnumerator());
        }


        /// <summary>
        /// This is a custom enumerator for the NodeList. It returns a GraphNode while enumerating over the internal dictionary, instead of a KeyValuePair.
        /// </summary>
        //public struct NodeListEnumerator : IDisposable
        //    , IEnumerator<KeyValuePair<int, GraphNode<T>>>
        //{
        //    private Dictionary<int, GraphNode<T>>.Enumerator dictionaryEnumerator;

        //    public NodeListEnumerator(Dictionary<int, GraphNode<T>>.Enumerator dictionaryEnumerator)
        //    {
        //        this.dictionaryEnumerator = dictionaryEnumerator;
        //    }

        //    //IEnumerator interface
        //    private GraphNode<T> Current
        //    {
        //        get
        //        {
        //            return dictionaryEnumerator.Current.Value;
        //        }
        //    }
            
        //    //object IEnumerator.Current
        //    //{
        //    //    get 
        //    //    {
        //    //        return Current;
        //    //    }
        //    //}

        //    bool IEnumerator.MoveNext()
        //    {
        //        return dictionaryEnumerator.MoveNext();
        //    }

        //    //public KeyValuePair<NodeValueType, GraphNode<NodeValueType>>
        //    //    IEnumerator<KeyValuePair<NodeValueType, GraphNode<NodeValueType>>>.Current
        //    //{
        //    //    get { return dictionaryEnumerator.Current; }
        //    //}

        //    public void Dispose()
        //    {
        //        dictionaryEnumerator.Dispose();
        //    }

        //    public void Reset()
        //    {
        //        throw new System.NotSupportedException();
        //    }



        //    KeyValuePair<int, GraphNode<T>> IEnumerator<KeyValuePair<int, GraphNode<T>>>.Current
        //    {
        //        get { return Current; }
        //    }

        //    object IEnumerator.Current
        //    {
        //        get { throw new NotImplementedException(); }
        //    }
        //}

    }
}
