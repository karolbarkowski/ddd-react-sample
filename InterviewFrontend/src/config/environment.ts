interface Environment {
  productsApiUrl: string;
}

function getEnvVariable(key: string): string {
  const value = import.meta.env[key];

  if (value === undefined) {
    throw new Error(`Missing environment variable: ${key}. `);
  }

  return value;
}

function validateEnvironment(): Environment {
  return {
    productsApiUrl: getEnvVariable("VITE_PRODUCTS_API_URL"),
  };
}

export const env = validateEnvironment();
