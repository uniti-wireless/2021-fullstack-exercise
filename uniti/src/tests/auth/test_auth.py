import pytest
from httpx import AsyncClient
from fastapi import status
from main import app

from utils.urls import AuthURL

@pytest.mark.asyncio
async def test_request_token():
    async with AsyncClient(app=app, base_url="http://test") as ac:
        endpoint = AuthURL.REQUEST_TOKEN
        payload = {
            "username": "john",
            "password": "password123"
        }
        response = await ac.post(endpoint, json=payload)
        assert response.status_code == status.HTTP_200_OK


@pytest.mark.asyncio
async def test_request_token_failed():
    async with AsyncClient(app=app, base_url="http://test") as ac:
        endpoint = AuthURL.REQUEST_TOKEN
        payload = {
            "username": "failed",
            "password": "failed"
        }
        response = await ac.post(endpoint, json=payload)
        assert response.status_code == status.HTTP_401_UNAUTHORIZED
