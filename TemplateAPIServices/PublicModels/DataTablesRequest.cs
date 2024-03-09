using System.Collections.Generic;

namespace TemplateAPIServices.PublicModels {
  public class DataTablesRequest {
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string Type { get; set; }
    public DataTableSort Sort { get; set; }
    public IList<DataTableFilter> Filters { get; set; }
  }

  public class DataTableFilter {
    public string Prop { get; set; }
    public string Value { get; set; }
    public FilterType Type { get; set; }
  }

  public class DataTableSort {
    public string Prop { get; set; }

    public string Order { get; set; }
  }

  public class DataTableResponse<T> {
    public IList<T> Data { get; set; }
    public int TotalCount { get; set; }
  }

  public enum FilterType {
    String = 1,
    Boolean
  }
}