import pytest
from httpx import AsyncClient
from fastapi import status
from main import app

from utils.urls import CustomerURL
from utils.urls import AuthURL
from utils.jwt_tool.jwt_handler import signJWT


JWT_TOKEN : str = signJWT("test")
headers = {
    "Authorization": f"Bearer {JWT_TOKEN}"
}


@pytest.mark.asyncio
async def test_get_customer_unauthenticated():
    async with AsyncClient(app=app, base_url="http://test") as ac:
        endpoint = CustomerURL.GET_CUSTOMER
        response = await ac.get(endpoint)
        assert response.status_code == status.HTTP_403_FORBIDDEN


@pytest.mark.asyncio
async def test_get_customer_wo_filter():
    async with AsyncClient(app=app, base_url="http://test") as ac:
        endpoint = CustomerURL.GET_CUSTOMER
        response = await ac.get(endpoint, headers=headers)
        assert response.status_code == status.HTTP_200_OK


@pytest.mark.asyncio
async def test_get_customer_w_pagination():
    async with AsyncClient(app=app, base_url="http://test") as ac:
        endpoint = CustomerURL.GET_CUSTOMER
        query_params = {
            "page": 2,
            "size": 10
        }
        response = await ac.get(endpoint, params=query_params, headers=headers)
        assert response.status_code == status.HTTP_200_OK


@pytest.mark.asyncio
async def test_get_customer_w_tag_filter():
    async with AsyncClient(app=app, base_url="http://test") as ac:
        endpoint = CustomerURL.GET_CUSTOMER
        query_params = {
            "tags": ["commercial", "bar"]
        }
        response = await ac.get(endpoint, params=query_params, headers=headers)
        response_json = response.json()
        for data in response_json["items"]:
            assert set(query_params["tags"]).intersection(set(data["tags"]))
        assert response.status_code == status.HTTP_200_OK


@pytest.mark.asyncio
async def test_get_customer_w_num_employee_filter_range():
    async with AsyncClient(app=app, base_url="http://test") as ac:
        endpoint = CustomerURL.GET_CUSTOMER
        query_params = {
            "num_employees": "1-10"
        }
        response = await ac.get(endpoint, params=query_params, headers=headers)
        response_json = response.json()
        range = query_params["num_employees"].split("-")
        for data in response_json["items"]:
            assert (
                int(data["num_employees"]) >= int(range[0]) and 
                int(data["num_employees"]) <= int(range[1]))
        assert response.status_code == status.HTTP_200_OK


@pytest.mark.asyncio
async def test_get_customer_w_num_employee_filter_gt():
    async with AsyncClient(app=app, base_url="http://test") as ac:
        endpoint = CustomerURL.GET_CUSTOMER
        query_params = {
            "num_employees": "gt50"
        }
        response = await ac.get(endpoint, params=query_params, headers=headers)
        response_json = response.json()
        for data in response_json["items"]:
            assert int(data["num_employees"]) > 50
        assert response.status_code == status.HTTP_200_OK


@pytest.mark.asyncio
async def test_get_customer_w_num_employee_filter_specifc_num():
    async with AsyncClient(app=app, base_url="http://test") as ac:
        endpoint = CustomerURL.GET_CUSTOMER
        query_params = {
            "num_employees": 1
        }
        response = await ac.get(endpoint, params=query_params, headers=headers)
        response_json = response.json()
        for data in response_json["items"]:
            assert int(data["num_employees"]) == query_params["num_employees"]
        assert response.status_code == status.HTTP_200_OK


@pytest.mark.asyncio
async def test_get_customer_w_mix_filter():
    async with AsyncClient(app=app, base_url="http://test") as ac:
        endpoint = CustomerURL.GET_CUSTOMER
        query_params = {
            "num_employees": 1,
            "tags": ["commercial", "bar"]
        }
        response = await ac.get(endpoint, params=query_params, headers=headers)
        response_json = response.json()
        for data in response_json["items"]:
            assert set(query_params["tags"]).intersection(set(data["tags"]))
            assert int(data["num_employees"]) == query_params["num_employees"]
        assert response.status_code == status.HTTP_200_OK
