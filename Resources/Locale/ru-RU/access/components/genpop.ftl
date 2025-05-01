genpop-prisoner-id-expire = Вы отбыли приговор! Теперь вы можете выйти из тюрьмы через турникеты и забрать свои вещи.
genpop-prisoner-id-popup-not-served = Приговор ещё не отбыт!
genpop-prisoner-id-crime-default = [Редактировать]
genpop-prisoner-id-examine-wait =
    Вы отбыли { $minutes } { $minutes ->
        [1] минуту
        [few] минуты
       *[other] минут
    } { $seconds } { $seconds ->
        [1] секунду
        [few] секунды
       *[other] секунд
    } из { $sentence } { $sentence ->
        [1] минуту
        [few] минуты
       *[other] минут
    } вашего приговора за "{ $crime }".
genpop-prisoner-id-examine-wait-perm = Вы отбываете вечный приговор за "{ $crime }".
genpop-prisoner-id-examine-served = Вы отбыли свой приговор за "{ $crime }".
genpop-locker-name-default = шкаф заключённого
genpop-locker-desc-default = Это защищённый шкафчик для персональных вещей заключённого во время их времени в тюрьме.
genpop-locker-name-used = prisoner closet ({ $name })
genpop-locker-desc-used = Это защищённый шкафчик для персональных вещей заключённого во время их времени в тюрьме. Содержит вещи { $name }.
genpop-locker-ui-label-name = [bold]Имя осуждённого:[/bold]
genpop-locker-ui-label-sentence = [bold]Время приговора в минутах:[/bold] [color=gray](0 для вечного)[/color]
genpop-locker-ui-label-crime = [bold]Преступление:[/bold]
genpop-locket-ui-button-done = Готово
genpop-locker-action-end-early = Завершить приговор досрочно
genpop-locker-action-clear-id = Очистить ID
genpop-locker-action-reset-sentence = Сброс времени приговора ({ NATURALFIXED($percent, 0) }% отбыто)
