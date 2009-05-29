// 
// Deserializer.cs
//  
// Author:
//       Scott Peterson <lunchtimemama@gmail.com>
// 
// Copyright (c) 2009 Scott Peterson
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

using Mono.Upnp.Control;
using Mono.Upnp.Xml;

namespace Mono.Upnp
{
    public class Deserializer
    {
        readonly XmlDeserializer deserializer;
        Root root;
        
        public Deserializer (XmlDeserializer xmlDeserializer)
        {
            if (xmlDeserializer == null) throw new ArgumentNullException ("xmlDeserializer");
            
            this.deserializer = xmlDeserializer;
        }
        
        public XmlDeserializer XmlDeserializer {
            get { return deserializer; }
        }
        
        public Root Root {
            get { return root; }
        }
        
        protected internal virtual Root DeserializeRoot (Uri url)
        {
            return null;
        }
        
        protected virtual Root DeserializeRoot (Uri url, XmlDeserializationContext context)
        {
            root = CreateRoot (url);
            if (root != null) {
                ((IXmlDeserializable)root).Deserialize (context);
            }
            return root;
        }
        
        protected virtual Root CreateRoot (Uri url)
        {
            return new Root (this, url);
        }
        
        protected internal virtual Device DeserializeDevice (XmlDeserializationContext context)
        {
            var device = CreateDevice ();
            if (device != null) {
                ((IXmlDeserializable)device).Deserialize (context);
            }
            return device;
        }
        
        protected virtual Device CreateDevice ()
        {
            return new Device (this);
        }
        
        protected internal virtual Service DeserializeService (XmlDeserializationContext context)
        {
            var service = CreateService ();
            if (service != null) {
                ((IXmlDeserializable)service).Deserialize (context);
            }
            return service;
        }
        
        protected virtual Service CreateService ()
        {
            return new Service (this);
        }
        
        protected internal virtual Icon DeserializeIcon (XmlDeserializationContext context)
        {
            var icon = CreateIcon ();
            if (icon != null) {
                ((IXmlDeserializable)icon).Deserialize (context);
            }
            return icon;
        }
        
        protected virtual Icon CreateIcon ()
        {
            return new Icon (this);
        }
        
        protected internal virtual ServiceAction DeserializeAction (ServiceController controller, XmlDeserializationContext context)
        {
            var action = CreateAction (controller);
            if (action != null) {
                ((IXmlDeserializable)action).Deserialize (context);
            }
            return action;
        }
        
        protected virtual ServiceAction CreateAction (ServiceController controller)
        {
            return new ServiceAction (controller);
        }
        
        protected internal virtual StateVariable DeserializeStateVariable (ServiceController controller, XmlDeserializationContext context)
        {
            var state_variable = CreateStateVariable (controller);
            if (state_variable != null) {
                ((IXmlDeserializable)state_variable).Deserialize (context);
            }
            return state_variable;
        }
        
        protected virtual StateVariable CreateStateVariable (ServiceController controller)
        {
            return new StateVariable (controller);
        }
        
        protected internal virtual ServiceController GetServiceController (Service service)
        {
            if (service == null) throw new ArgumentNullException ("service");
            
            return deserializer.Deserialize<ServiceController> (null, (context) => DeserializeServiceController (service, context));
        }
        
        protected virtual ServiceController DeserializeServiceController (Service service, XmlDeserializationContext context)
        {
            var service_controller = CreateServiceController (service);
            if (service_controller != null) {
                ((IXmlDeserializable)service_controller).Deserialize (context);
            }
            return service_controller;
        }
        
        protected virtual ServiceController CreateServiceController (Service service)
        {
            return new ServiceController (this, service);
        }
        
        protected internal virtual Uri DeserializeUrl (XmlDeserializationContext context)
        {
            if (context == null) throw new ArgumentNullException ("context");
            if (root == null)
                    throw new InvalidOperationException ("You must deserialize a device description before deserializing a URL.");
            
            var url = context.Reader.ReadString ();
            if (Uri.IsWellFormedUriString (url, UriKind.Absolute)) {
                return new Uri (url);
            } else if (Uri.IsWellFormedUriString (url, UriKind.Relative)) {
                return new Uri (root.UrlBase, url);
            } else {
                throw new UpnpDeserializationException ("The URL is neither absolute nor relative: " + url);
            }
        }
        
        internal bool IsDisposed { get; private set; }
        
        internal event EventHandler<DisposedEventArgs> Disposed;
        
        internal void Dispose ()
        {
        }
        
        internal void CheckDisposed ()
        {
        }
    }
}