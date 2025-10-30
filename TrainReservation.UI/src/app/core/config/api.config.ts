export function getApiUrl() {
  return '/api/';
}

export const apiProvider = {
  provide: 'API_URL',
  useFactory: getApiUrl,
  deps: [],
};
