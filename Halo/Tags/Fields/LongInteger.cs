using System.IO;
using System.Xml;
using System.CodeDom;
using System.Diagnostics;
using Interfaces.Factory;
using System.ComponentModel;

namespace Games.Halo.Tags.Fields
{
  public class LongInteger : IField, INotifyPropertyChanged
  {
    private int value = 0;

    public int Value
    {
      get { return value; }
      set { this.value = value; NotifyPropertyChanged("Value"); }
    }
  	
  	public LongInteger() { }
  	public LongInteger(int value) { this.value = value; }

    public void Read(BinaryReader reader)
    {
      string pos = null; ;
      if (TagDebug.EnableReadDebug) pos = string.Format("    {0:X}:  ", (int)reader.BaseStream.Position + 0x40);
      value = reader.ReadInt32();
      if (TagDebug.EnableReadDebug) Trace.WriteLine(pos + ToString());
    }

    public void Write(BinaryWriter writer)
    {
      writer.Write(value);
    }
    public override string ToString()
    {
      return (string.Format("LongInteger: {0}", value));
    }
    private void NotifyPropertyChanged(string info)
    {
      if (PropertyChanged != null)
        PropertyChanged(this, new PropertyChangedEventArgs(info));
    }

    public event PropertyChangedEventHandler PropertyChanged;
    public static implicit operator int(LongInteger left)
    {
      return left.value;
    }
  }

  public class LongIntegerCodeGenerator : IFieldCodeGenerator
  {
    private string name;

    public string Name
    {
      get { return name; }
    }

    public LongIntegerCodeGenerator(XmlNode node)
    {
      this.name = node.Attributes["name"].InnerText;
    }

    public CodeMemberField GeneratePrivateMember()
    {
      return GlobalMethods.GeneratePrivateMember("LongInteger", name, "new LongInteger()");
    }

    public CodeMemberProperty GeneratePublicAccessors()
    {
      return GlobalMethods.GeneratePublicAccessors("LongInteger", name, Accessors.Both);
    }

    public string GenerateMetadataInitializer()
    {
      return null;
    }

    public CodeStatement GenerateConstructorLogic()
    {
      return null;
    }
  }
}
