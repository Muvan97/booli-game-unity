IMAGE_NAME=test-telegram-bot
PORT=8080
PORT2=6000

build:
	docker build -t $(IMAGE_NAME) .

run: build
	docker run -d --name telegram-bot -p ${PORT}:${PORT2} test-telegram-bot

stop:
	docker stop telegram-bot

clean:
	docker rm telegram-bot

logs:
	docker logs -f telegram-bot