"""Run against the locally running Part 1 API; uses only Python's standard library."""
import json
import ssl
import sys
from urllib.error import HTTPError
from urllib.request import Request, urlopen

base = (sys.argv[1] if len(sys.argv) > 1 else 'https://localhost:7250').rstrip('/')
# Trust the self-signed certificate only for these local development requests.
context = ssl._create_unverified_context() if base.startswith('https://localhost:') else None


def request(method, path, expected, body=None):
    data = None if body is None else json.dumps(body).encode()
    req = Request(base + path, data=data, method=method,
                  headers={'Content-Type': 'application/json'} if data else {})
    try:
        response = urlopen(req, context=context, timeout=10)
    except HTTPError as error:
        response = error
    with response:
        raw = response.read()
        assert response.status == expected, (method, path, response.status, raw)
        headers = response.headers
    print(f'PASS {method} {path}: {expected}')
    return (json.loads(raw) if raw else None), headers


items, _ = request('GET', '/catalog', 200)
assert [(x['name'], x['price']) for x in items] == [('Shirt', 29.99), ('Shorts', 44.99)]
item, _ = request('GET', '/catalog/42', 200)
assert item['id'] == 42 and item['name'] == 'Shirt'
body = {'name': 'Shoes', 'description': 'Ohio State shoes', 'brand': 'Nike', 'price': 129.99}
posted, headers = request('POST', '/catalog', 201, body)
assert headers['Location'] == '/catalog/42'
assert all(posted[key] == value for key, value in body.items())
rating = {'stars': 5, 'userName': 'Patrick', 'review': 'Great shirt!'}
rated, _ = request('POST', '/catalog/42/ratings', 200, rating)
assert rated['id'] == 42 and rated['ratings'] == [rating]
for method, payload in [('PUT', body), ('DELETE', None)]:
    result, _ = request(method, '/catalog/42', 204, payload)
    assert result is None
request('GET', '/catalog/not-an-integer', 404)
spec, _ = request('GET', '/swagger/v1/swagger.json', 200)
assert sum(len(operations) for operations in spec['paths'].values()) == 6
with urlopen(base + '/swagger/index.html', context=context, timeout=10) as response:
    assert response.status == 200 and b'Swagger UI' in response.read()
print('PASS Swagger UI and all six documented operations')
# Part 1 must remain stateless after the write demonstrations.
items, _ = request('GET', '/catalog', 200)
assert len(items) == 2 and all(x['ratings'] == [] for x in items)
print('All Part 1 HTTP checks passed.')
