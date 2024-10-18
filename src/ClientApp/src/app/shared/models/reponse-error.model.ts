export type ReponseError = {
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
  BadRequest,
  ServerError,
}
