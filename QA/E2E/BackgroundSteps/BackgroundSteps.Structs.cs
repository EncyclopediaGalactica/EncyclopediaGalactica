namespace EncyclopediaGalactica.SourceFormats.QA.E2E.BackgroundSteps;

public partial class BackgroundSteps
{
    private struct Keys
    {
        public const string EndpointUrl = "endpoint_url";
        public const string OperationUrl = "operation_url";
        public const string SourceFormatNodeName = "sourceformatnode_name";
        public const string SdkRequestModelBuilder = "sdk_requestmodel_builder";
        public const string SdkRequestModel = "sdk_request_model";
        public const string SdkOperationResult = "sdk_operation_result";
        public const string SdkOperationName = "sdk_operation_name";
        public const string SdkType = "SdkType";
    }

    private struct Operations
    {
        public const string AddNewSourceFormatNode = "add_new_sourceformatnode";
    }

    private struct ExceptionTypes
    {
        public const string SdkModelsException = "SdkModelsException";
    }

    private struct SdkType
    {
        public const string SourceFormatSdk = "SourceFormatSdk";
    }
}