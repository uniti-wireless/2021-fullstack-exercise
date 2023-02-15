
from fastapi import status

from utils.tools import ServiceException

class AuthException:

    INVALID_CREDENTIALS = ServiceException(status.HTTP_401_UNAUTHORIZED, "Invalid username or password")
    INVALID_TOKEN = ServiceException(status.HTTP_403_FORBIDDEN, "Invalid or Expired token")