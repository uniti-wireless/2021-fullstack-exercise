
import time
import jwt
import os

JWT_SECRET = os.environ.get("JWT_SECRET", "568547a4b6cdb154f4f5374cd3b1105acd7c63b9")
JWT_ALGORITHM = os.environ.get("JWT_ALGORITHM", "HS256")
TOKEN_EXPIRY = os.environ.get("TOKEN_EXPIRY", 300) # 5 min 


def signJWT(user: str):
    payload = {
        "user": user,
        "expiry": time.time() + int(TOKEN_EXPIRY)
    }
    return jwt.encode(payload, JWT_SECRET, algorithm=JWT_ALGORITHM)


def decodeJWT(token: str):
    try:
        decode_token = jwt.decode(token, JWT_SECRET, algorithms=JWT_ALGORITHM)
        return decode_token if decode_token["expiry"] >= time.time() else None
    except:
        return None
