namespace ViewModels.Shared;

public record PagedListResult<T>(List<T> Data, RequestFeatures.MetaData MetaData);