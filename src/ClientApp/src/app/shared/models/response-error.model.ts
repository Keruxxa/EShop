export type ResponseError = {
  entityError: EntityError;
  errorType: ErrorType;
};

interface EntityError {
  message: string;
}

export enum ErrorType {
  NotFound,
  InvalidRequest,
  Duplicate,
  Forbidden,
  BadRequest,
  Unauthorized,
  ServerError,
}
