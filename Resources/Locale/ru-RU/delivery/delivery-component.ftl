delivery-recipient-examine = Предназначено для {$recipient}, {$job}.
delivery-already-opened-examine = Уже вскрыто.
delivery-recipient-no-name = Безымянный
delivery-recipient-no-job = Неизвестно

delivery-unlocked-self = Вы разблокировали {$delivery} отпечатком пальца.
delivery-opened-self = Вы открываете {$delivery}.
delivery-unlocked-others = {CAPITALIZE($recipient)} {GENDER($recipient) ->
        [male] разблокировал
        [female] разблокировала
        [epicene] разблокировали
       *[neuter] разблокировало
        } {$delivery} используя свой отпечаток пальца.
delivery-opened-others = {CAPITALIZE($recipient)} {GENDER($recipient) ->
        [male] открыл
        [female] открыла
        [epicene] открыли
       *[neuter] открыло
        } {$delivery}.

delivery-unlock-verb = Разблокировать
delivery-open-verb = Открыть
