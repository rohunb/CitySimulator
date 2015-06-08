/*
  GraphNodeList.cs
  RoadsPrototype
  Created by Rohun Banerji on June 07, 2015.
  Copyright (c) 2015 Rohun Banerji. All rights reserved.
*/

using System.Collections;
using System.Collections.Generic;
using System;

namespace DataStructures
{
    /// <summary>
    /// The node list is implemented as a dictionary, with the value for the node being hashed for quick access
    /// </summary>
    /// <typeparam name="NodeValueType"></typeparam>
    public class GraphNodeList<NodeValueType> : IEnumerable
    {
        private Dictionary<NodeValueType, GraphNode<NodeValueType>> nodeTable = new Dictionary<NodeValueType, GraphNode<NodeValueType>>();

        
        public GraphNode<NodeValueType> this[NodeValueType key]
        {
            get
            {
                return nodeTable[key];
            }
        }

        /// <summary>
        /// Add a new node
        /// </summary>
        /// <param name="node"></param>
        public void Add(GraphNode<NodeValueType> node)
        {
            nodeTable.Add(node.Value, node);
        }

        /// <summary>
        /// Remove a node
        /// </summary>
        /// <param name="node"></param>
        public bool Remove(GraphNode<NodeValueType> node)
        {
            return nodeTable.Remove(node.Value);
        }

        public bool ContainsValue(NodeValueType value)
        {
            return nodeTable.ContainsKey(value);
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
            return new NodeListEnumerator(nodeTable.GetEnumerator());
        }


        /// <summary>
        /// This is a custom enumerator for the NodeList. It returns a GraphNode while enumerating over the internal dictionary, instead of a KeyValuePair.
        /// </summary>
        public struct NodeListEnumerator : IEnumerator, IDisposable
            //, IEnumerator<KeyValuePair<NodeValueType, GraphNode<NodeValueType>>>
        {
            private Dictionary<NodeValueType, GraphNode<NodeValueType>>.Enumerator dictionaryEnumerator;

            public NodeListEnumerator(Dictionary<NodeValueType, GraphNode<NodeValueType>>.Enumerator dictionaryEnumerator)
            {
                this.dictionaryEnumerator = dictionaryEnumerator;
            }

            //IEnumerator interface
            private GraphNode<NodeValueType> Current
            {
                get
                {
                    return dictionaryEnumerator.Current.Value;
                }
            }
            
            object IEnumerator.Current
            {
                get 
                {
                    return Current;
                }
            }

            bool IEnumerator.MoveNext()
            {
                return dictionaryEnumerator.MoveNext();
            }

            //public KeyValuePair<NodeValueType, GraphNode<NodeValueType>>
            //    IEnumerator<KeyValuePair<NodeValueType, GraphNode<NodeValueType>>>.Current
            //{
            //    get { return dictionaryEnumerator.Current; }
            //}

            public void Dispose()
            {
                dictionaryEnumerator.Dispose();
            }

            public void Reset()
            {
                throw new System.NotSupportedException();
            }

            
        }

    }

    
}
