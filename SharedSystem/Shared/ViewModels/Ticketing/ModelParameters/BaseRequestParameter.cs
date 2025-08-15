using RequestFeatures;

namespace ViewModels.Ticketing.ModelParameters;

public class BaseRequestParameter : RequestParameters;

public class StatusParameters : BaseRequestParameter;
public class TicketSubjectParameters : BaseRequestParameter;
public class TicketParameters : BaseRequestParameter;
public class TicketMessageParameters : BaseRequestParameter;