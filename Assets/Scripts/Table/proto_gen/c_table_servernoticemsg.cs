//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Option: missing-value detection (*Specified/ShouldSerialize*/Reset*) enabled
    
// Generated from: c_table_servernoticemsg.proto
namespace Table
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"SERVER_NOTICE_MSG")]
  public partial class SERVER_NOTICE_MSG : global::ProtoBuf.IExtensible
  {
    public SERVER_NOTICE_MSG() {}
    

    private uint? _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint id
    {
      get { return _id?? default(uint); }
      set { _id = value; }
    }
    //Here has been replaced by XXMMLLDeleter
    [global::System.ComponentModel.Browsable(false)]
    public bool idSpecified
    {
      get { return _id != null; }
      set { if (value == (_id== null)) _id = value ? id : (uint?)null; }
    }
    private bool ShouldSerializeid() { return idSpecified; }
    private void Resetid() { idSpecified = false; }
    

    private string _content;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"content", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string content
    {
      get { return _content?? ""; }
      set { _content = value; }
    }
    //Here has been replaced by XXMMLLDeleter
    [global::System.ComponentModel.Browsable(false)]
    public bool contentSpecified
    {
      get { return _content != null; }
      set { if (value == (_content== null)) _content = value ? content : (string)null; }
    }
    private bool ShouldSerializecontent() { return contentSpecified; }
    private void Resetcontent() { contentSpecified = false; }
    
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"SERVER_NOTICE_MSG_ARRAY")]
  public partial class SERVER_NOTICE_MSG_ARRAY : global::ProtoBuf.IExtensible
  {
    public SERVER_NOTICE_MSG_ARRAY() {}
    
    private readonly global::System.Collections.Generic.List<Table.SERVER_NOTICE_MSG> _rows = new global::System.Collections.Generic.List<Table.SERVER_NOTICE_MSG>();
    [global::ProtoBuf.ProtoMember(1, Name=@"rows", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Table.SERVER_NOTICE_MSG> rows
    {
      get { return _rows; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}