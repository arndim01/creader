﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ONEReader.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ONEReader.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; standalone=&quot;yes&quot;?&gt;
        ///&lt;pattern.nyk.project version=&quot;1.0.0.0&quot;&gt;
        ///	&lt;patternkey name=&quot;carrier&quot;&gt;
        ///		&lt;item&gt;NYK&lt;/item&gt;
        ///	&lt;/patternkey&gt;
        ///	&lt;patternkey name=&quot;main_search&quot;&gt;
        ///		&lt;item&gt;6.rate&lt;/item&gt;
        ///		&lt;item&gt;6-1.generalrate&lt;/item&gt;
        ///		&lt;item&gt;6-2.specialrate&lt;/item&gt;
        ///		&lt;item&gt;6-3.originarbitrary&lt;/item&gt;
        ///		&lt;item&gt;6-4.destinationarbitrary&lt;/item&gt;
        ///	&lt;/patternkey&gt;
        ///	&lt;patternkey name=&quot;main_search_type&quot;&gt;
        ///		&lt;item&gt;Last[1,3]&lt;/item&gt;
        ///	&lt;/patternkey&gt;
        ///  &lt;patternkey name=&quot;kill_search&quot;&gt;
        ///    &lt;item&gt;g.o.h&lt;/i [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string nyk_pattern_version_1 {
            get {
                return ResourceManager.GetString("nyk_pattern_version_1", resourceCulture);
            }
        }
    }
}