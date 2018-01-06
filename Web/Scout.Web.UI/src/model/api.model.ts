export enum OperationResult {
  Unknown = 0,
  Success = 1,
  Failure = 2,
  Error = 4
}

export class Response<T> {
  result: OperationResult;
  message: string;
  responseBody: T;
}
