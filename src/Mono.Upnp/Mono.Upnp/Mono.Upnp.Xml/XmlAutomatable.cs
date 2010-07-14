// 
// XmlAutomatable.cs
//  
// Author:
//       Scott Thomas <lunchtimemama@gmail.com>
// 
// Copyright (c) 2009 Scott Thomas
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;

namespace Mono.Upnp.Xml
{
    public abstract class XmlAutomatable : XmlDeserializable, IXmlSerializable
    {
        void IXmlSerializable.SerializeSelfAndMembers (XmlSerializationContext context)
        {
            SerializeSelfAndMembers (context);
        }
        
        void IXmlSerializable.SerializeMembersOnly (XmlSerializationContext context)
        {
            SerializeMembersOnly (context);
        }
        
        protected abstract void SerializeSelfAndMembers (XmlSerializationContext context);
        
        protected abstract void SerializeMembersOnly (XmlSerializationContext context);

        internal static void AutoSerializeObjectAndMembers<T> (T @this, XmlSerializationContext context)
        {
            if (context == null) {
                throw new ArgumentNullException ("context");
            }

            context.AutoSerializeObjectAndMembers (@this);
        }

        internal static void AutoSerializeMembersOnly<T> (T @this, XmlSerializationContext context)
        {
            if (context == null) {
                throw new ArgumentNullException ("context");
            }

            context.AutoSerializeMembersOnly (@this);
        }
    }
    
    public abstract class XmlAutomatable<TContext> : XmlDeserializable, IXmlSerializable<TContext>
    {
        void IXmlSerializable<TContext>.SerializeSelfAndMembers (XmlSerializationContext<TContext> context)
        {
            SerializeSelfAndMembers (context);
        }
        
        void IXmlSerializable<TContext>.SerializeMembersOnly (XmlSerializationContext<TContext> context)
        {
            SerializeMembersOnly (context);
        }
        
        protected abstract void SerializeSelfAndMembers (XmlSerializationContext<TContext> context);
        
        protected abstract void SerializeMembersOnly (XmlSerializationContext<TContext> context);

        internal static void AutoSerializeObjectAndMembers<T> (T @this, XmlSerializationContext<TContext> context)
        {
            if (context == null) {
                throw new ArgumentNullException ("context");
            }

            context.AutoSerializeObjectAndMembers (@this);
        }

        internal static void AutoSerializeMembersOnly<T> (T @this, XmlSerializationContext<TContext> context)
        {
            if (context == null) {
                throw new ArgumentNullException ("context");
            }

            context.AutoSerializeMembersOnly (@this);
        }
    }
}
