using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using AggregateSource.Testing;

namespace NAuthorize.Tests.Infrastructure {
  public class SpecificationMarkdownWriter {
    readonly IndentedTextWriter _writer;

    public SpecificationMarkdownWriter(TextWriter writer) {
      if (writer == null) throw new ArgumentNullException("writer");
      _writer = new IndentedTextWriter(writer);
    }

    public void Write(TestSpecification specification) {
      if(!String.IsNullOrEmpty(specification.Name)) {
        _writer.WriteLine("#" + specification.Name);
        _writer.WriteLine();
      }
      _writer.Indent++;
      _writer.WriteLine();
      if (specification.Givens.Any()) {
        _writer.WriteLine("Given ");
        _writer.WriteLine();
        _writer.Indent++;
        foreach (var given in specification.Givens.Select(item => item.Item2)) {
          _writer.WriteLine(given.ToString());
        }
        _writer.Indent--;
        _writer.WriteLine();
      }
      _writer.WriteLine("When");
      _writer.WriteLine();
      _writer.Indent++;
      _writer.WriteLine(specification.When.ToString());
      _writer.Indent--;
      _writer.WriteLine();
      _writer.WriteLine("Then");
      _writer.WriteLine();
      _writer.Indent++;
      foreach (var then in specification.Thens.Select(item => item.Item2)) {
        _writer.WriteLine(then.ToString());
      }
      _writer.Indent--;
      _writer.WriteLine();
      _writer.Indent--;
      _writer.WriteLine("----");
    }
  }
}
