﻿using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using DefaultDocumentation.Helper;
using ICSharpCode.Decompiler.CSharp.OutputVisitor;
using ICSharpCode.Decompiler.Output;
using ICSharpCode.Decompiler.TypeSystem;

namespace DefaultDocumentation.Model
{
    internal sealed class EnumDocItem : TypeDocItem
    {
        private static readonly CSharpAmbience CodeAmbience = new CSharpAmbience
        {
            ConversionFlags =
                ConversionFlags.ShowAccessibility
                | ConversionFlags.ShowDeclaringType
                | ConversionFlags.ShowDefinitionKeyword
                | ConversionFlags.ShowModifiers
                | ConversionFlags.ShowTypeParameterList
                | ConversionFlags.ShowTypeParameterVarianceModifier
        };

        public EnumDocItem(DocItem parent, ITypeDefinition type, XElement documentation)
            : base(parent, type, documentation)
        { }

        public override void WriteDocumentation(DocumentationWriter writer, IReadOnlyDictionary<string, DocItem> items)
        {
            writer.WriteHeader();
            writer.WritePageTitle(Name, Type.Kind.ToString());

            writer.Write(this, Documentation.GetSummary());

            writer.WriteLine("```C#");
            writer.WriteLine(CodeAmbience.ConvertSymbol(Type));
            writer.WriteLine("```");

            //inheritance

            // attribute

            writer.WriteDocItems(items.Values.OfType<EnumFieldDocItem>().Where(i => i.Parent == this), "### Fields");

            writer.Write("### Example", Documentation.GetExample(), this);
            writer.Write("### Remarks", Documentation.GetRemarks(), this);
        }
    }
}