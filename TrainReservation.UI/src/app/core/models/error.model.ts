export interface ModelError {
  statusCode: number,
  errorMessage: string,
  errors?: Record<string, string[]>,
  errorStack: string,
  timestamp: string,
  path: string,
  traceId: string
}
