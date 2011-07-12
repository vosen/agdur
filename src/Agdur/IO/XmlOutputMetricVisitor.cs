﻿using System.Xml;
using Agdur.Abstractions;
using Agdur.Introspection;

namespace Agdur.IO
{
    /// <summary>
    /// Provides XML output of the results of a metric.
    /// </summary>
    public class XmlOutputMetricVisitor : IMetricVisitor
    {
        private readonly XmlWriter writer;

        /// <summary>
        /// Creates a new instance of the <see cref="XmlOutputMetricVisitor"/> class.
        /// </summary>
        /// <param name="writer"></param>
        public XmlOutputMetricVisitor(XmlWriter writer)
        {
            Ensure.ArgumentNotNull(writer, "writer");
            this.writer = writer;
        }

        /// <inheritdoc/>
        public void Visit(SingleValueMetric metric)
        {
            Ensure.ArgumentNotNull(metric, "metric");
            writer.WriteElementString(metric.Name, metric.GetValue());
        }

        /// <inheritdoc/>
        public void Visit(MultipleValueMetric metric)
        {
            Ensure.ArgumentNotNull(metric, "metric");

            var values = metric.GetValues();
            for (int index = 0; index < values.Count; index++)
            {
                writer.WriteStartElement(metric.Name);
                writer.WriteAttributeString("index", index.ToString());

                string value = values[index];
                writer.WriteString(value);

                writer.WriteEndElement();
            }
        }
    }
}