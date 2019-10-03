import { Injectable } from '@angular/core';
import { ISong } from '../songs/isong';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SongsService {
  constructor(private http: HttpClient) {

  }
  getSongs(): ISong[] {
    return [
    {
        id: 289,
        name: "aaaaftrdg",
        youTubeUrl: "asd",
        presentationId: "1G9Lcwr3xabjhmnMzEB-vF6MV5HLPk_mS_71KdJFAVnU"
    },
    {
        id: 308,
        name: "aaacc",
        youTubeUrl: null,
        presentationId: "19K31reKjyaaFD59PiLA1pfJbayK5c1Yybel8FTgV3NM"
    },
    {
        id: 273,
        name: "aaacd",
        youTubeUrl: null,
        presentationId: "183zXudLDOJvSX-QBR7bILnTVm4Q1QPm8oTjNeR-fX8Y"
    },
    {
        id: 278,
        name: "aaasdasd",
        youTubeUrl: null,
        presentationId: "1gAivzAQ_UIkXSXfsO0KWEixV4FDbs5cSsUwvhea4kgI"
    },
    {
        id: 287,
        name: "aaasdhjk",
        youTubeUrl: null,
        presentationId: "1udyL1CR5yhbcm-KHf8Rle470JH2BGhih2DyK5qQvRmY"
    },
    {
        id: 292,
        name: "aaassd",
        youTubeUrl: null,
        presentationId: "1Zc3sF8T672-UwYWd-thmN5W6MoZrN92WgPBEyR9aT-8"
    },
    {
        id: 285,
        name: "aaassdAAaa",
        youTubeUrl: null,
        presentationId: "1DdyiMipQJBV5O9RuBKQVCERzekdnNGKCsQdKzxEa3OE"
    },
    {
        id: 297,
        name: "aaazasd",
        youTubeUrl: null,
        presentationId: "1b0vqq8b4f402jS2si-6SjOLK3cEWFviqITY6uD2q0Dw"
    },
    {
        id: 290,
        name: "aasdasdads",
        youTubeUrl: null,
        presentationId: "1on1t5WiNKCNwhySX5A2ggxqLAK_p7sMEpE6smVOLrOc"
    },
    {
        id: 279,
        name: "aazaaaaasdsdssdas",
        youTubeUrl: null,
        presentationId: "1gNtFZzel2xMKJ07usS-mHKSPETG3M3sidWOGN7_3HP0"
    },
    {
        id: 303,
        name: "aazbbaa",
        youTubeUrl: null,
        presentationId: "1fhkRBBB9fMNCFyPzmSL3K5cHW8ndDje20OUvp-I3rnM"
    },
    {
        id: 49,
        name: "Abba Ojcze",
        youTubeUrl: null,
        presentationId: "1hDh2gV_VhNKDgnNbRXRDJBuHHCslzW8w2MjJSrGl46s"
    },
    {
        id: 274,
        name: "abbbbaaabc",
        youTubeUrl: null,
        presentationId: "1MewCFThUakubTsPb88a4FT_puR-fsAsXXgvJJIFYLEE"
    },
    {
        id: 50,
        name: "Alleluja (Niech zabrzmi Panu)",
        youTubeUrl: null,
        presentationId: "1t2rwnqVGf8C4RCzOmyZqMOxYmQP8FyjgXLiAgADQZpk"
    },
    {
        id: 51,
        name: "Alleluja, Alleluja, Amen Amen, Alleluja",
        youTubeUrl: null,
        presentationId: "1Y_f9fgmUOEFMpGAUSGxC4Vdxdapf38m17zW-JeRwp2k"
    },
    {
        id: 294,
        name: "ararara",
        youTubeUrl: null,
        presentationId: "11Bc2OS2ZooIAu9vn2kKGSnnDX_yMnENup5ls7CNTtxM"
    },
    {
        id: 305,
        name: "asd",
        youTubeUrl: null,
        presentationId: "1P-jYA8n-oVQFunPJS8xSufBAvVfTeWjldwzfYQRZn6E"
    },
    {
        id: 281,
        name: "asdaaaa",
        youTubeUrl: null,
        presentationId: "1Rx6_gsgEb9bnjZO3jkD9nv4c80TXS39QlriSHDxRTAU"
    },
    {
        id: 284,
        name: "asdasd",
        youTubeUrl: null,
        presentationId: "1LPwCLZDr-KYHAscEayS_d6TUA9ox6RSOIJQsHDf83mI"
    },
    {
        id: 298,
        name: "az",
        youTubeUrl: null,
        presentationId: "1oBbvToNB-sH2ePLsNmMbkw7smFR6MNz2pDf4fGqGHMU"
    },
    {
        id: 275,
        name: "azaaacdeasd",
        youTubeUrl: null,
        presentationId: "1uHaK_b3CtNpMzOG5bgz_QR36t2ronkh6_HdiJ5FkrFM"
    },
    {
        id: 306,
        name: "azxccz",
        youTubeUrl: null,
        presentationId: "1wCcuAiq9qnU_gBa7YawVd_FkVo5xIz6_FLK5WgulTME"
    },
    {
        id: 52,
        name: "Będę śpiewał Ci",
        youTubeUrl: null,
        presentationId: "1E4iSSVPJkldnzGT12t8fei4GHz5Ipa22H8qPlpycGpQ"
    },
    {
        id: 53,
        name: "Będę śpiewał Tobie",
        youTubeUrl: null,
        presentationId: "10bZ4H8wutveuI_ZqbSvaWpQRRojnOBY1-Ur4l8rZuRE"
    },
    {
        id: 54,
        name: "Będziemy tańczyć",
        youTubeUrl: null,
        presentationId: "1osH9skWQZB-Epf2coFG0o69qgiPJldE5IskHTXgHqtY"
    },
    {
        id: 55,
        name: "Blisko, blisko, blisko jesteś",
        youTubeUrl: null,
        presentationId: "1UKLQ8GS36TkuduYQ-gdWlAfc5-0eaDR7azJ1wwD9fnU"
    },
    {
        id: 59,
        name: "Bo góry mogą ustąpić",
        youTubeUrl: null,
        presentationId: "12aV-0-TsI0YK-KhlM2APPu4bTMwSe7VcQuFvTNnsVyk"
    },
    {
        id: 60,
        name: "Bo jak śmierć potężna jest miłość",
        youTubeUrl: null,
        presentationId: "1FOwludPk8jzaPiyFzgIIH3mIA9_XA6U0WMnl1-_13Q4"
    },
    {
        id: 62,
        name: "Bóg nasz Pan",
        youTubeUrl: null,
        presentationId: "1-zgtKP-ym0tIZYn0QJUGvvZRnQ5vGfedylDd42rSspg"
    },
    {
        id: 63,
        name: "Bóg tak umiłował świat",
        youTubeUrl: null,
        presentationId: "1-Qmzpyrvn2ET6swwi1ygLAT2AdGwdFjvQm9c7wfvBe0"
    },
    {
        id: 61,
        name: "Boże Twa łaska",
        youTubeUrl: null,
        presentationId: "1QhO8Y7w5FpJ9lPJVIMAx-hMbYzhczHi8OEGgpNaB0nI"
    },
    {
        id: 64,
        name: "Była cicha i piękna jak wiosna",
        youTubeUrl: null,
        presentationId: "1EeRnYSS9D_s7AQWw6VCQzQGfGnPdVSZxSWJeEGVQZ8c"
    },
    {
        id: 56,
        name: "Błogosław, duszo moja Pana",
        youTubeUrl: null,
        presentationId: "1bhi2W5yC4tJF1NMuCf4rYiw39Wlb5lTk-Ri6I0IvgHU"
    },
    {
        id: 57,
        name: "Błogosławcie Pana wszystkie Jego dzieła",
        youTubeUrl: null,
        presentationId: "1VbHKKmH_dwvIYVINhUN7m7a3IhlbjRVSmEZLHXARp_M"
    },
    {
        id: 58,
        name: "Błogosławione jest Imię Twe",
        youTubeUrl: null,
        presentationId: "1RyZ1hTwEfh76CDzDJSzcTvM4YkDUJqYYeavP84xNLnA"
    },
    {
        id: 65,
        name: "Cała ziemio wołaj",
        youTubeUrl: null,
        presentationId: "1cj_MgQ2Yp0BiwzMtsbcEJLVo4Vm177Y5LubLTM_yfxo"
    },
    {
        id: 67,
        name: "Chcę wywyższać Imię Twe",
        youTubeUrl: null,
        presentationId: "1RgNfx7dhOdXwqC2SojP2RpJj_YAod0XP6TQsuGhEKGs"
    },
    {
        id: 68,
        name: "Chlebie najcichszy",
        youTubeUrl: null,
        presentationId: "1oX9EDeO3czvOXLa8xv2jrn9WkWE7qcz-OoiFWdTWULI"
    },
    {
        id: 69,
        name: "Chrystus Pan karmi nas",
        youTubeUrl: null,
        presentationId: "1EZydrKVh0w1HQSVSpvMVS-tVq1eFzkK-6hNmMyY1YTo"
    },
    {
        id: 70,
        name: "Chrystus Pan, Boży Syn",
        youTubeUrl: null,
        presentationId: "1cMXavzGfwYz43JYpDyX7xGTmqGoUaE9S1G2cDau09RA"
    },
    {
        id: 71,
        name: "Chwal Adonai",
        youTubeUrl: null,
        presentationId: "1v3I-N7YC0MQRSUPc6aJqv4DhaLRv4Eh9RUBPL2wPEgU"
    },
    {
        id: 73,
        name: "Chwalcie Pana niebios",
        youTubeUrl: null,
        presentationId: "1Z_zzp5614g6OSrJf3e8rce6QzmAVFRQCJmNqMLn158M"
    },
    {
        id: 72,
        name: "Chwalcie łąki umajone",
        youTubeUrl: null,
        presentationId: "1Kk7tRPYQYFqe9OrFtBB2j0J4gHVooIbvllnAYwNvQag"
    },
    {
        id: 74,
        name: "Chwalę Ciebie, Panie",
        youTubeUrl: null,
        presentationId: "1IxNF1GI6jtpP6ak-V1ZbGyqg2LXaA4QrQT3uL_I7wAk"
    },
    {
        id: 75,
        name: "Chwała Bogu Ojcu",
        youTubeUrl: null,
        presentationId: "1NSTfOsGeMPBneV1XgohgHAKNrWVcRPaF4PvTolCkuhg"
    },
    {
        id: 76,
        name: "Ciebie całą duszą pragnę",
        youTubeUrl: null,
        presentationId: "1jRp33hdCJ-NDW3AAVH7aRj068MJU_xNwK96S5DKSt34"
    },
    {
        id: 77,
        name: "Do Ciebie Panie",
        youTubeUrl: null,
        presentationId: "1roXlt3n2bC4IwfcrVwWz9LXe_LxtDM3cdkb43x3j2qA"
    },
    {
        id: 78,
        name: "Do mnie wróć",
        youTubeUrl: null,
        presentationId: "1WFfVLKJUfEswGPFYz-YOT72yR8Lh2cy6JN8JxoC2zmI"
    },
    {
        id: 79,
        name: "Dobry jest Pan",
        youTubeUrl: null,
        presentationId: "1ykQCDOpGLK70MDHPT3XWHwLTTUM_VxuT5i-I64Xa65c"
    },
    {
        id: 80,
        name: "Dotknij Panie moich oczu",
        youTubeUrl: null,
        presentationId: "1hbqqqQ8zI2zgiVhUknodL1UrckIOYSPFz2OrRYWxe24"
    },
    {
        id: 81,
        name: "Duchu miłości",
        youTubeUrl: null,
        presentationId: "1jS9msI4P_7SNo-ltOnUBP3Hm_YO1x6VlLVGKFvStQsw"
    },
    {
        id: 82,
        name: "Duchu święty (o przyjdź i rozpal nas)",
        youTubeUrl: null,
        presentationId: "1emJYRPPOpXXJ8wL3aHbwcPHeywfsttxXxx0CRue1z5w"
    },
    {
        id: 83,
        name: "Duchu święty ogarnij mnie",
        youTubeUrl: null,
        presentationId: "1jrs9cPaDyfLbM3yW4_JuPlPFPBleluscWZhvaLvMdDA"
    },
    {
        id: 84,
        name: "Duchu święty powiej wiatrem (ORZECH ALERT!!)",
        youTubeUrl: null,
        presentationId: "10zRxZdv-9ZYo6PetGPVhNocJrWcjbht2PYGye224OSA"
    },
    {
        id: 85,
        name: "Duchu święty przyjdź (Niech wiara zagości)",
        youTubeUrl: null,
        presentationId: "1IkrEr3GCRrH-4WSDVLCqfvOmrNSOfak_6jYlnfnffHg"
    },
    {
        id: 86,
        name: "Duchu święty, cudownym wiatrem jesteś",
        youTubeUrl: null,
        presentationId: "1EJHFrQNKN-HX9jJUzkHXSt9Xk5WTMRZazXzPf1jSrmY"
    },
    {
        id: 87,
        name: "Duszo ma, Pana chwal",
        youTubeUrl: null,
        presentationId: "1HVE6OQjRoLMoqj8cY8obQEDIC3wQUzXFm89f2g3dlfI"
    },
    {
        id: 89,
        name: "Dzięki, o Panie! Składamy dzięki",
        youTubeUrl: null,
        presentationId: "1I6qrpQMMaHGg3d0LiWfPuUC6-iUjYUocZgyy3NnHWjo"
    },
    {
        id: 88,
        name: "Dzielmy się wiarą jak chlebem",
        youTubeUrl: null,
        presentationId: "1XySDcToOpr563aUj3PjVXPuSuYfilWCnxheKCshQSzY"
    },
    {
        id: 90,
        name: "Gdy kiedyś Pan",
        youTubeUrl: null,
        presentationId: "1Eob6mfHocBwJDPrSdCroLiKlMuWCtd3xj3KgwAS0R_M"
    },
    {
        id: 91,
        name: "Gdy klęczę przed Tobą",
        youTubeUrl: null,
        presentationId: "1aM3lEvxqOXipEBvzksZE_oZgy3yyUceJ8qcFKatimZo"
    },
    {
        id: 92,
        name: "Gdy schodzimy się",
        youTubeUrl: null,
        presentationId: "1lIYkNAu3SyG8VZ9bXFfZw61KM2Axb0K9mOq12DLN0Jw"
    },
    {
        id: 128,
        name: "GLORIA!!! (Na niedzielę i święta)",
        youTubeUrl: null,
        presentationId: "18xpnMLuC9peM3qVcl3Sd03l1mOQoeZ3MQu92AkpQNeY"
    },
    {
        id: 93,
        name: "Godzien o godzien jest Bóg",
        youTubeUrl: null,
        presentationId: "1n8t3mWhJwXQ3k8VOQeq86rzGtO_6mN8fnpltJNIMsjw"
    },
    {
        id: 94,
        name: "Gwiazdo zaranna",
        youTubeUrl: null,
        presentationId: "1Sj0SRwLIXkkJeJRGdO9pVv6YzLkLNBu4ydmJSWcZQ0o"
    },
    {
        id: 95,
        name: "Idź w pokoju",
        youTubeUrl: null,
        presentationId: "1FnhdSDKTjZ3qzlteJEpI4WbYBUi0StJJyXNzEm3Mc3E"
    },
    {
        id: 309,
        name: "ilhjhjkkjh",
        youTubeUrl: null,
        presentationId: "1Ba-2qcqMtjpRfHC4bdqweN3NrG93eBdyQyLWFbGzbJI"
    },
    {
        id: 96,
        name: "Ja wierzę, że to Jezus",
        youTubeUrl: null,
        presentationId: "1cz83RiIcffGMEk0z0zu31ydo3QW3P_FkpylzuOPKUks"
    },
    {
        id: 97,
        name: "Jak Dawid",
        youTubeUrl: null,
        presentationId: "1owydBzXJVl3Ha6By4F9TBJPXHI2MaU3Yw7vvu3XRgeA"
    },
    {
        id: 98,
        name: "Jak dobrze jest dziękować Ci",
        youTubeUrl: null,
        presentationId: "1OQnGcwF9M45ApOXdmB-EHdRl18vHPGSg_ZUDmRanZuE"
    },
    {
        id: 99,
        name: "Jak ożywczy deszcz",
        youTubeUrl: null,
        presentationId: "1uEpSbkYAsOKZBgXDkA1MnXkb2xALPF2UrEzROMPkIh4"
    },
    {
        id: 100,
        name: "Jak wielki jest Bóg",
        youTubeUrl: null,
        presentationId: "1q8E0rgmfg_ue7qUstPgltopuMTbkuPl1D4ZBiSHhxgY"
    },
    {
        id: 101,
        name: "Jeden chleb",
        youTubeUrl: null,
        presentationId: "1_YE0phxb6ijQHn2uBVPWnXpGOaslVXEz_uUsTcBP4NI"
    },
    {
        id: 102,
        name: "Jeden jest tylko Pan",
        youTubeUrl: null,
        presentationId: "1AMTL_NRDMNWJJJw_ueXYb2VlNa3EFwXFIQCSfe-Ir-w"
    },
    {
        id: 103,
        name: "Jedyny Pan, prawdziwy Bóg",
        youTubeUrl: null,
        presentationId: "1Ibq0863rtL6pdzHAFT6jRR03uUokaYt60POh4MCK4ig"
    },
    {
        id: 104,
        name: "Jest jedno ciało",
        youTubeUrl: null,
        presentationId: "1hdgkN8nMFNtb638l1o_Et-ipN4i7S9WEjmhJxeoJdW0"
    },
    {
        id: 105,
        name: "Jest zakątek na tej Ziemii",
        youTubeUrl: null,
        presentationId: "17U7pDlRLptxaUEVpt4pxF423GcfHDQ3iaIm7WZRBVwk"
    },
    {
        id: 129,
        name: "Jesteś blisko mnie (Twoja miłość)",
        youTubeUrl: null,
        presentationId: "1xhvku0td5PgsWLF5XPC1bIHS6aKG7h6EgJcWF2QNj-Y"
    },
    {
        id: 106,
        name: "Jesteś Królem",
        youTubeUrl: null,
        presentationId: "1PpepDrRMjjwLnNiQiwACgzHZMpUWgK857z_ycq1q5gI"
    },
    {
        id: 107,
        name: "Jesteś życiem mym",
        youTubeUrl: null,
        presentationId: "1xa4HPcLLkROsa8x6_fqD-QsLE5I22OvtdgOUPajgKFE"
    },
    {
        id: 108,
        name: "Jezu mój Jezu",
        youTubeUrl: null,
        presentationId: "16OXNad4-BaEnvBMrGuFb2n5enoPcCDs_hvjQAtg11E8"
    },
    {
        id: 109,
        name: "Jezu Tyś jest",
        youTubeUrl: null,
        presentationId: "1y8RXhUPKonXkkpoytLnB100cuhGXc7GJIWmJnk9Rqh0"
    },
    {
        id: 110,
        name: "Jezus Chrystus moim Panem jest",
        youTubeUrl: null,
        presentationId: "1avM6Hp2OiasLKbDUtKXvAxS2m5mUeh84flJJze_27-0"
    },
    {
        id: 111,
        name: "Jezus Chrystus to Panów Pan",
        youTubeUrl: null,
        presentationId: "1JFwptrqUTHQYpq5JFY4MyGmcgZ6MDv2fzEyH_VFzfQ8"
    },
    {
        id: 112,
        name: "Jezus daje nam zbawienie",
        youTubeUrl: null,
        presentationId: "1DNXNBTkBYsxqZzHr3WTJrmQaiuqLipeLoIi0fC7MOfs"
    },
    {
        id: 113,
        name: "Jezus najwyższe Imię",
        youTubeUrl: null,
        presentationId: "1IICoM-CDfc-raWjmW-Ab6FMKka4mYWnajbvNjB307JU"
    },
    {
        id: 114,
        name: "Jezus Namaszczony Pan (Twe imię jak miód)",
        youTubeUrl: null,
        presentationId: "1N9RTPxyiI7RI8xKAV5ofOgrrqpBl5WKxhCjBKwLn7aM"
    },
    {
        id: 310,
        name: "julka,m julka",
        youTubeUrl: null,
        presentationId: "19tvWhLelJtFdPjFwfnyxf6Af7K7WAn9B7FVYCbn6uHc"
    },
    {
        id: 115,
        name: "Już teraz we mnie kwitną Twe ogrody",
        youTubeUrl: null,
        presentationId: "1wDpkvdIpdW6D2roWIJvHqrBx_9q4TUZJjnnwlVEY9HA"
    },
    {
        id: 116,
        name: "Każdy dzień upewnia mnie",
        youTubeUrl: null,
        presentationId: "1N0vTjiDy0-HVqn56wnkEz7yOyugsXnqN7qK3Im8xZWY"
    },
    {
        id: 117,
        name: "Każdy spragniony",
        youTubeUrl: null,
        presentationId: "1bB4-bDEPRuuJx0Cuxg_HQGBW6l6r6JlTVxhU-WmXWKU"
    },
    {
        id: 118,
        name: "Kiedy masz chwile smutne (Matce która pod krzyżem)",
        youTubeUrl: null,
        presentationId: "15UHMfOofFn1bWkI6vpGyBWYr5u_FnjYGhVUDpryUlgA"
    },
    {
        id: 66,
        name: "Kiedy w jasną, spokojną - O Boże",
        youTubeUrl: null,
        presentationId: "1aWM6X6QTyNJTM09Z9Yb4KFRLTsNmsVWHv_NbKyH09eI"
    },
    {
        id: 119,
        name: "Kiedyś wino i chleb",
        youTubeUrl: null,
        presentationId: "1LEdCl3O__4cxshQ3zrZvuSot1lu3dWcSy0DXD7L6EDw"
    },
    {
        id: 120,
        name: "Kocham więc nie muszę się bać",
        youTubeUrl: null,
        presentationId: "1k0cRH9n_yuTDynBilWXmXXDoff3YYS8C-4obie0YDd4"
    },
    {
        id: 121,
        name: "Kto spożywa moje ciało",
        youTubeUrl: null,
        presentationId: "1s_WWQiHnUJQQG89N1QDcGWvBVVRtSxkzpH5T1qblQ44"
    },
    {
        id: 122,
        name: "Kto spragniony jest",
        youTubeUrl: null,
        presentationId: "1uSQwg3dmDFd3qpKTahSEbOE18xQXBLlV09CfUe0Okiw"
    },
    {
        id: 123,
        name: "Laudato si",
        youTubeUrl: null,
        presentationId: "1Ra0J0r--yJyhfjM8iPXo7jFxaHbsjoezguxzwd-7xAk"
    },
    {
        id: 126,
        name: "Maryjo śliczna Pani",
        youTubeUrl: null,
        presentationId: "1li9XvMoeZsiMr7unwssuLdv0P0RrQl2okTJLY6aIKLM"
    },
    {
        id: 127,
        name: "Matko która nas znasz",
        youTubeUrl: null,
        presentationId: "1TVlWFHow1NucdazabjwTYwuNv60QwEd9YxMI1iBdd3A"
    },
    {
        id: 130,
        name: "Memu Bogu, Królowi",
        youTubeUrl: null,
        presentationId: "1C2t8I6S8V54NBrTHnvNVd1ziSJCZaJmHnGd9fB7tyso"
    },
    {
        id: 131,
        name: "Miłość Twa",
        youTubeUrl: null,
        presentationId: "1qX020kXZ6dkBGke7uEeJclZs8iYL4HQVIxXAc9rQ3U8"
    },
    {
        id: 132,
        name: "Mój Jezu, mój Zbawco",
        youTubeUrl: null,
        presentationId: "1HhJmmeMOlbXAFKXlEiuUjpCwcyTwAn1xwLXyaZ535rs"
    },
    {
        id: 133,
        name: "Mój Pan mocą moją jest",
        youTubeUrl: null,
        presentationId: "11-eVrHTaCEz2d-5BwnJ--q5D7IE8LxGjyxf8xOqiEoc"
    },
    {
        id: 134,
        name: "Mój zbawiciel",
        youTubeUrl: null,
        presentationId: "1SWfNQighDNOY3BDOJ1HN5iaOEi4rZ3bogHkCfyQKt_I"
    },
    {
        id: 135,
        name: "Mów do mnie Panie",
        youTubeUrl: null,
        presentationId: "1NidsDkodgPTAkGxLiZvWERGOw_V2tSqIe8OHcUgBw_4"
    },
    {
        id: 136,
        name: "Nasz Bóg jest potężny w mocy swej",
        youTubeUrl: null,
        presentationId: "1J683Trs1C6lsMpHEzoT1B44u6C8OYE4OlJBnVSBws_g"
    },
    {
        id: 137,
        name: "Nasz Bóg jest wielki",
        youTubeUrl: null,
        presentationId: "1h66JkETLRQ5ArI-ZDtTQP1hNUS5opdd3aAc-CNWJVw8"
    },
    {
        id: 138,
        name: "Nic nie musisz mówić nic",
        youTubeUrl: null,
        presentationId: "1k5LMqxLavCO2uC40OPk127EcnLqDNKn1pLWu5RLNbYA"
    },
    {
        id: 139,
        name: "Nie bój się (Zostań tu)",
        youTubeUrl: null,
        presentationId: "1Ikhw461OsIpZxbjz4mo5xrboeyAMCmC7uciDtMbohfQ"
    },
    {
        id: 140,
        name: "Nie bój się, wypłyń na głębię",
        youTubeUrl: null,
        presentationId: "1m_-kqrTQw3-62eCq6ErRGfQFr779OM0vETvkyFmJNMM"
    },
    {
        id: 141,
        name: "Nie ma za trudnych spraw (Zanim powiem słowo)",
        youTubeUrl: null,
        presentationId: "1FFOKhHU-VAd5lTFRm196QQ4u1kCYBUAdDwmtas1QpAw"
    },
    {
        id: 142,
        name: "Nie zabraknie mi nigdy chleba",
        youTubeUrl: null,
        presentationId: "1JEV9Nqlz54YsE6PVlZekEjoc17-iHJf3Me6eggrtXbA"
    },
    {
        id: 143,
        name: "Niech oblicze Twe",
        youTubeUrl: null,
        presentationId: "1IkiMoKsP8t76V0tLeAXMSNa5PjLGV0dptRf1REGN8iE"
    },
    {
        id: 144,
        name: "Niech Twój święty duch (i niech spłynie)",
        youTubeUrl: null,
        presentationId: "1MaeZoq7KKvmPLfNeUBF71rwgTYh2igO2-QThqHIs498"
    },
    {
        id: 145,
        name: "Niechaj Cię Panie",
        youTubeUrl: null,
        presentationId: "1tv2tlB6OoTb6lla4PAzuVB00eI82FSV5EqeLoSD_Gy0"
    },
    {
        id: 146,
        name: "Niechaj miłość Twa",
        youTubeUrl: null,
        presentationId: "1AF5s8h_DBPyQ1VAMmJfRNACWDPyPHKvsiuKLKy4hwVU"
    },
    {
        id: 147,
        name: "Niechaj zstąpi Duch Twój",
        youTubeUrl: null,
        presentationId: "1xPcuzCb6wU2HfTyB7g0dt0Pr_W70213X61kMeN5fhEQ"
    },
    {
        id: 148,
        name: "Niepojęta łaska",
        youTubeUrl: null,
        presentationId: "19vSW8adTeKZ-Kwi3vMmPf3CxjUaEV0NHwQvQDV-JAOc"
    },
    {
        id: 149,
        name: "Niepojęty, niezmierzony",
        youTubeUrl: null,
        presentationId: "1qHlaf7IBjA0zd3i3mC9AXGJmKXHKpkxvE3mKXsW2c-4"
    },
    {
        id: 150,
        name: "O Panie Ty nam dajesz",
        youTubeUrl: null,
        presentationId: "1sP2bOxlz6Y_ojVjtHqzy9iG49crZgNhllp60HcfHVUQ"
    },
    {
        id: 151,
        name: "O piękności niestworzona",
        youTubeUrl: null,
        presentationId: "1OEEN9bkNf07ju8KeCnaLLF3EyL9lCt_rPcJwL4v3Q6s"
    },
    {
        id: 152,
        name: "O stworzycielu, Duchu przyjdź",
        youTubeUrl: null,
        presentationId: "1QMUtCPBlMcJc4xJeRSrZbS2NF9DOiqM7uHBvU6oFXJg"
    },
    {
        id: 153,
        name: "O wychwalajcie Go",
        youTubeUrl: null,
        presentationId: "1jyb91wOq5H3sY3-r1BJBm617iEUt2NkOohnRjzlSY6w"
    },
    {
        id: 154,
        name: "Oczekuję Ciebie Panie",
        youTubeUrl: null,
        presentationId: "1VdSDIjky60UnP-d6SzUx_ZNiPsGDgfNztOY9L3VH_Fs"
    },
    {
        id: 155,
        name: "Oczyść serce me",
        youTubeUrl: null,
        presentationId: "11BAYfCsIkmqeaVKHq1_NqG3wkKLzE0aBO2LGyj9TpFQ"
    },
    {
        id: 156,
        name: "Oddajmy cześć",
        youTubeUrl: null,
        presentationId: "19eRFYdWGD6Sbv50GCjbH25lq7-StrLDnBbcjWTD6alg"
    },
    {
        id: 157,
        name: "Odnów mnie",
        youTubeUrl: null,
        presentationId: "1C7UvyJGSrtMGTPj_5L1IgFPJSVW1-QGzXIMU3NS7bWA"
    },
    {
        id: 158,
        name: "Ofiaruję Tobie",
        youTubeUrl: null,
        presentationId: "1CJnXcbLSeDFU6KmIBzCmQ_GdLKqBfxhmr3Saei1Niy0"
    },
    {
        id: 159,
        name: "Oświeć drogę mą",
        youTubeUrl: null,
        presentationId: "1rLeTNFe4Nhp4nRwIfvfBPn7-Dqv9FbiJ96VigLgKuX8"
    },
    {
        id: 160,
        name: "Oto ja poślij mnie",
        youTubeUrl: null,
        presentationId: "1m8Dgwz6giLs76KshnEfj2fw08_TjdxtfeJlXtLbSc1I"
    },
    {
        id: 161,
        name: "Oto jest dzień",
        youTubeUrl: null,
        presentationId: "1k88JNTdfs6jgix_kb1l4nEDTxv66j2m22nTeHOKV1-0"
    },
    {
        id: 162,
        name: "Oto stoję u drzwi",
        youTubeUrl: null,
        presentationId: "1In065gsIsKlv5E3eDJbfsVb6VLThUN3RbZfjE91iNm4"
    },
    {
        id: 163,
        name: "Otwórz me oczy o Panie",
        youTubeUrl: null,
        presentationId: "1QI0toO_aUJyV4MPxCmPSXn4uKhk7mvfOg4VLU9EDcd8"
    },
    {
        id: 164,
        name: "Pa-a-an jest pasterzem moim",
        youTubeUrl: null,
        presentationId: "18COFNs0yqhGQKHw9FC4piqjFPWtQX8YM4yIscINGKyI"
    },
    {
        id: 165,
        name: "Pan - jest moim pasterzem",
        youTubeUrl: null,
        presentationId: "1F7Fzemtc16WXi0d2YdE7-xQTAzcVImWT2xEZ7axx42M"
    },
    {
        id: 166,
        name: "Pan blisko jest",
        youTubeUrl: null,
        presentationId: "1k09dKl-u7ofG8KphpL52Ore-_29aWiF8ljZBvJ3zvVk"
    },
    {
        id: 167,
        name: "Pan mnie strzeże",
        youTubeUrl: null,
        presentationId: "1fUaAh-B74nSlRZqqtm-fyX4R0iXog8uQj6eFVLFLylk"
    },
    {
        id: 168,
        name: "Pan wywyższony",
        youTubeUrl: null,
        presentationId: "104qzIo1Ksg3wZ9LE7GxZnnqDxbnvvslspQHVzbl1z_A"
    },
    {
        id: 169,
        name: "Panie dobry jak chleb",
        youTubeUrl: null,
        presentationId: "1nNk3nUeU3E7GOlB6ZJzZuHUcKwR1-vm39Vinne5BrI8"
    },
    {
        id: 170,
        name: "Panie mój przychodzę dziś",
        youTubeUrl: null,
        presentationId: "1FBK5Xb7zdRxWlOgQxUfiWj68j9po26qq1NktZNH8QJA"
    },
    {
        id: 171,
        name: "Panie pozostań",
        youTubeUrl: null,
        presentationId: "1jYR0-IZuXN1DnT4Tuz9DmxNbicmkD0Vk7yUM7u8Im4g"
    },
    {
        id: 172,
        name: "Panu chwała i cześć",
        youTubeUrl: null,
        presentationId: "1MoX5Y7L3-oN6xrjtxodrt1lSy85Df7Xoi_lSfO_Mba0"
    },
    {
        id: 173,
        name: "Pewnej nocy łzy",
        youTubeUrl: null,
        presentationId: "19FcxTEQtylXLYHPvjZy-uqe6phxoY0IWDEIgIEid3TE"
    },
    {
        id: 174,
        name: "Podnieś mnie Jezu",
        youTubeUrl: null,
        presentationId: "17v223ttXMoESRpNTD3gRH77bpbZO_0-DrLcBC6dJ6zM"
    },
    {
        id: 175,
        name: "Pokój Wam nie bójcie się",
        youTubeUrl: null,
        presentationId: "1Dqqh8ah2P7Se-IKrAmyN1eajzJWxuptVWn9eUtW6VK8"
    },
    {
        id: 176,
        name: "Pomódl się Miriam",
        youTubeUrl: null,
        presentationId: "1DgsxGbJEzMFAabLdnq0J2qsHQlgnXv5oXriXa7o5Vdo"
    },
    {
        id: 177,
        name: "Powietrzem moim jest",
        youTubeUrl: null,
        presentationId: "1bCRam6kp309pRbemGyJLhuoyqt7tHw3Ea4cREebsuJo"
    },
    {
        id: 178,
        name: "Prawda jedyna",
        youTubeUrl: null,
        presentationId: "1dEJLjId_rq_pjg9VJMzLYwRROTX0zwzmyl6xe8NHZvY"
    },
    {
        id: 179,
        name: "Przed obliczem Pana uniżmy się",
        youTubeUrl: null,
        presentationId: "1YyqP_zEe8i_5E8MwsOBVTmbv016H2AKnFBzEvRN7Ci0"
    },
    {
        id: 180,
        name: "Przed Tronem Twym",
        youTubeUrl: null,
        presentationId: "1s8FYQPx6zldQuK4rZCvSmSXetqHJSYJ-agyezmHVshk"
    },
    {
        id: 181,
        name: "Przychodzisz Panie",
        youTubeUrl: null,
        presentationId: "1qkkz2xMfsvLKpIs95cdCm6SltKaEJPwvJhRiRC9YmQQ"
    },
    {
        id: 182,
        name: "Przyjaciela mam",
        youTubeUrl: null,
        presentationId: "1Pea1eM16zRqXyOD-mr3RDRQY5lk6SOPyKoBIyfDPlbU"
    },
    {
        id: 183,
        name: "Przyjacielu",
        youTubeUrl: null,
        presentationId: "1N6i8iw2vHb6ETZdvluBFe6XLEeeYzt4g3FQ_IVeaf68"
    },
    {
        id: 184,
        name: "Przyjdź jak deszcz",
        youTubeUrl: null,
        presentationId: "1kNvcuXJH_7RJm8vgkfuug2hQnh4AaeghaQmttgcyofE"
    },
    {
        id: 185,
        name: "Przywołaj mnie, Panie",
        youTubeUrl: null,
        presentationId: "1qgku4Tm8kfQ0Hst8JtFLNKxZ1NMDXMsY4V4Rup3zTX4"
    },
    {
        id: 186,
        name: "Raduje się Dusza ma",
        youTubeUrl: null,
        presentationId: "1CZfITc6e6PVTx7ObHUrrRpMxvn5Zz4jC4hliLjGZQU0"
    },
    {
        id: 187,
        name: "Rozpięty na ramionach",
        youTubeUrl: null,
        presentationId: "1Uef6IVC8WtrWUANd5GH-ZPmak3xANFMIco5TAqIJJGg"
    },
    {
        id: 188,
        name: "Schowaj mnie",
        youTubeUrl: null,
        presentationId: "1LdBzOW68CVZFPOPl4q5z7sc9dhF0yVIeKO_YKLEGIIg"
    },
    {
        id: 189,
        name: "Serce wielkie nam daj",
        youTubeUrl: null,
        presentationId: "1RR3noesshqNUnXOH51geNGsOPIrzihXoBfa9HwXgfRI"
    },
    {
        id: 190,
        name: "Spocznij na nas, Duchu Pana",
        youTubeUrl: null,
        presentationId: "17MQddDWXxM1-h_tOJRPaUppYCUjAP9qqaPVmCYonvu4"
    },
    {
        id: 191,
        name: "Stoisz u naszych drzwi",
        youTubeUrl: null,
        presentationId: "1S_gpEaSG6aCfqRdlpfn18AM1H2tsaZGPpmUNS9LfRio"
    },
    {
        id: 192,
        name: "Stoję dziś",
        youTubeUrl: null,
        presentationId: "1yVAuSecfzo48PK2rIpAdQMPeyvQlYxAjgIfrCX-fxWI"
    },
    {
        id: 194,
        name: "Święte Imię Jezus",
        youTubeUrl: null,
        presentationId: "1J94u1VUmKVmuzN3oe4Ac7amuxW9xLjHThUQd45Ddods"
    },
    {
        id: 195,
        name: "Święty Święty... (Otwieram serce swe)",
        youTubeUrl: null,
        presentationId: "1zzc5NGur0o3zXaJ0idipqWBfouQaRlT7WokxcXu9Sho"
    },
    {
        id: 193,
        name: "Swojego Ducha Panie",
        youTubeUrl: null,
        presentationId: "11pd404XOpj9gBp8MYxX06ZWlNGIFZBghilhHhTmydz0"
    },
    {
        id: 196,
        name: "Ta krew",
        youTubeUrl: null,
        presentationId: "1XHhs0UYO6yDCgMQXrC03XDnlPVS-zrgHXBAdFol2G7Q"
    },
    {
        id: 197,
        name: "Tak jest mało czasu (zabierzesz mnie)",
        youTubeUrl: null,
        presentationId: "1Bt5yK30Pvzt3eDhhMRsYAl9P4np7qEcEmkt0skVPid4"
    },
    {
        id: 198,
        name: "Tak mnie skrusz",
        youTubeUrl: null,
        presentationId: "1foYtU4BSlkizsA8vzufjfrfoybTRFI8XpKq7Iwxe0M4"
    },
    {
        id: 199,
        name: "Tchnij moc",
        youTubeUrl: null,
        presentationId: "1dyT8mEacwu2HkS5c9d1BFXH1EN1rnA4QYARGMQPcYgk"
    },
    {
        id: 200,
        name: "Tobie chór Aniołów",
        youTubeUrl: null,
        presentationId: "1uY_v7NBSdWi3IqgRwiXxd3kJ91hM_Q-BflCScA4BUas"
    },
    {
        id: 201,
        name: "Twe światło jest na drodze mej",
        youTubeUrl: null,
        presentationId: "1TgOFL-CcnkcaXUoO91lPvQ5ESUVvkVaF9Q6pG6WzF2A"
    },
    {
        id: 202,
        name: "Ty jesteś chlebem żywym",
        youTubeUrl: null,
        presentationId: "1u2LzyIA3lHyGC9B6wG7T7J7pypbszrbhWj2V1K_Ru_g"
    },
    {
        id: 203,
        name: "Ty jesteś dobry",
        youTubeUrl: null,
        presentationId: "1jSgdFsWQxOpWuyntreow4owszaiuuoJabAWLHeOkUy4"
    },
    {
        id: 204,
        name: "Ty jesteś skałą",
        youTubeUrl: null,
        presentationId: "1I1-Ok14ybeAZfAHzehzN39ilLGgpZIdxAkMf3hjhYhk"
    },
    {
        id: 205,
        name: "Ty światłość dnia",
        youTubeUrl: null,
        presentationId: "1xZTeNqUrG1WyVNyclf8RTBqnTx6TSQ_3YkCIR88gwy4"
    },
    {
        id: 206,
        name: "Ty tylko mnie poprowadź",
        youTubeUrl: null,
        presentationId: "14Gas3ONsZBm4SH38f3DjyPUeGScrvZczjXaGwvV2Lzs"
    },
    {
        id: 207,
        name: "Ty wskazałeś drogę do miłości",
        youTubeUrl: null,
        presentationId: "1fS-chKVCvobc4KrGk7LJcu6Hn9peNEk_Pg-y4HZoAP8"
    },
    {
        id: 208,
        name: "Tyś jest Bóg, wywyższamy Cię",
        youTubeUrl: null,
        presentationId: "1jl9ez_9ooERIrVwYHhn-QcariVpUbZXIdZW2bpz-lSg"
    },
    {
        id: 209,
        name: "Ubi Caritas (Tam gdzie miłość)",
        youTubeUrl: null,
        presentationId: "1dezgJqt6-te1yZhVpl11bTHM09r1j2UVK3E04voRbKI"
    },
    {
        id: 210,
        name: "Ukaż mi Panie swą twarz",
        youTubeUrl: null,
        presentationId: "1JEBCIY90-KPcrCXLCxhEGDoEVmgnhCXLVmScZ80gHZc"
    },
    {
        id: 211,
        name: "Uwielbiajcie Pana ludzkich serc",
        youTubeUrl: null,
        presentationId: "1n_TEpCuiUAMi5NdZn4jh_kW1J595UoWfDVbFDC_lc7M"
    },
    {
        id: 212,
        name: "Uwielbiam Imię Twoje Panie",
        youTubeUrl: null,
        presentationId: "1ApOU3OjhYoCNXaYtoYJceLp8ia9QYwPtTVSYfyOiQjE"
    },
    {
        id: 213,
        name: "Uwielbiam Twoje Imię",
        youTubeUrl: null,
        presentationId: "1L_TnE38N8f9sHdchmG7bfxKcbB-kDGq8yJbQMVwcGfU"
    },
    {
        id: 214,
        name: "W cieniu Twoich rąk",
        youTubeUrl: null,
        presentationId: "11TwW50oiTnxiUC8rHvg5iSJ3ennzCBdNp86oBtRbjl0"
    },
    {
        id: 215,
        name: "W swe ramiona",
        youTubeUrl: null,
        presentationId: "1d7z8VPjEeZTLuIGpIFlQe9BvC_D8H8VLsn6WoonG1is"
    },
    {
        id: 216,
        name: "W swoim wielkim (Pieśń o nadziei)",
        youTubeUrl: null,
        presentationId: "1vIIu9I3ftJBA9HXX8mML9U6l5vxBNDxY7mS_E5qEYok"
    },
    {
        id: 217,
        name: "W Tobie jest światło",
        youTubeUrl: null,
        presentationId: "1nATaF0YYJHfcOVhSmtzyvWQCef-pR57XDctc8pJEEv4"
    },
    {
        id: 218,
        name: "Wejdźmy do Jego bram",
        youTubeUrl: null,
        presentationId: "1pWuFCqGd6SDDdj5hU1nbYjGgv-6Ra2tw712OEmV79Jk"
    },
    {
        id: 219,
        name: "Wielbi Dusza moja Pana (Magnificat)",
        youTubeUrl: null,
        presentationId: "12leePibDnuis2WDuR7UKde20OkVu9xpKgv-ZkUT3gjA"
    },
    {
        id: 220,
        name: "Wielbić Pana chcę",
        youTubeUrl: null,
        presentationId: "1wwgS0VtQfSI3WCz_CN2YtH7jLb8-_Kmn9WDNDpoG2xA"
    },
    {
        id: 221,
        name: "Wiele jest serc",
        youTubeUrl: null,
        presentationId: "1vlzrPqkEbKHua8u98xccZxbEXBMpOYVO6uZw5xmbmOM"
    },
    {
        id: 222,
        name: "Wierzę w Ciebie Panie",
        youTubeUrl: null,
        presentationId: "1In2UMhYD9Vqq9k293cSofjnDbeBtS10O0i38Wj2DKSw"
    },
    {
        id: 223,
        name: "Wszelka chwała",
        youTubeUrl: null,
        presentationId: "1bt2CqUJpQnwrXKH60MF9B8yOmMAv7I1ihygs6NVglm4"
    },
    {
        id: 224,
        name: "Wszyscy ludzie (Nadejdzie dzień)",
        youTubeUrl: null,
        presentationId: "1vSyV9JzPHuYdbLI1k8p54Ih8EXLhiQuvyBtqfwSgCeA"
    },
    {
        id: 225,
        name: "Wszystkie narody klaskajcie",
        youTubeUrl: null,
        presentationId: "1Vkd5YUOzSd0cc3nkoyonvv24Ylk5c3MccQU4Coo0I8o"
    },
    {
        id: 226,
        name: "Wszystko mogę w Tym",
        youTubeUrl: null,
        presentationId: "1MkvxsWwP6aGsl1sakpEFN-C8pZmyVqgzzomfzqpl3v8"
    },
    {
        id: 227,
        name: "Wykrzykujcie Bogu",
        youTubeUrl: null,
        presentationId: "1uLIlCt0gX_w-qgRB8qRI77wJ1DC-IG9DFePsPHVp5j4"
    },
    {
        id: 228,
        name: "Wykrzykujcie na cześć Pana",
        youTubeUrl: null,
        presentationId: "1RI9P2-CJVnGJPIjwBRHvsq12rO09Qj-NIfWPHfoNNs8"
    },
    {
        id: 229,
        name: "Wzywam Cię, Duchu przyjdź",
        youTubeUrl: null,
        presentationId: "1IspzPpR0aH1jRrkzIrAOLyW6aluUuJ7e9wqcfJ_-Iwg"
    },
    {
        id: 282,
        name: "zaa",
        youTubeUrl: null,
        presentationId: "1-MjyN4MrQlrCUCXLwsWJCfIileInufEKgFa9_8Uv5Ho"
    },
    {
        id: 299,
        name: "zaaa2",
        youTubeUrl: null,
        presentationId: "1xkybQ0GqOYL-cVj66X3nCqbO7py4okNV2QgHIwj_hPI"
    },
    {
        id: 300,
        name: "zaaa4",
        youTubeUrl: null,
        presentationId: "1cw3P00RgQkevhBummgAitZnp6Ich36IqYWBbcLZgfrI"
    },
    {
        id: 301,
        name: "zaaa5",
        youTubeUrl: null,
        presentationId: "1dt7ePOrjh8scS8UhGBxEuPvZCSIJM3hsW6kh8j5ELhk"
    },
    {
        id: 230,
        name: "Zaprowadź mnie tam",
        youTubeUrl: null,
        presentationId: "1364pJU8gGSaR2OvCz5l2Y8tnMZ2vLVv9k5K6OWkKY_4"
    },
    {
        id: 231,
        name: "Zaufaj Panu już dziś",
        youTubeUrl: null,
        presentationId: "1F9NJ06eooHZZAcDgEhCP6seX3lv2BgPB2OVJPbqyH8E"
    },
    {
        id: 232,
        name: "Zbawca",
        youTubeUrl: null,
        presentationId: "1YBpd4fYZPUHJH1oIbq58-6yerAKrJEcjXz3M6Vpwz4E"
    },
    {
        id: 233,
        name: "Ziemia, którą mi dajesz",
        youTubeUrl: null,
        presentationId: "1V16XL-_Skdd27LaNU4YcHYBvcAT0Pzyte2NrxfU-aJ4"
    },
    {
        id: 234,
        name: "Zmartwychwstał Pan",
        youTubeUrl: null,
        presentationId: "1mGOHxjsltT1kYXL3cbM-3tHI1QSMuuowmFPwdBWx90s"
    },
    {
        id: 288,
        name: "łaaas",
        youTubeUrl: null,
        presentationId: "1SXwU65eqouN6m18gaoCOCMxAwCzLdGkGOT4MEPiF8wA"
    },
    {
        id: 125,
        name: "Łaską jesteśmy zbawieni",
        youTubeUrl: null,
        presentationId: "1CJYzyCwgfgc0DCaw8vh38q2tugRdV7qw-OEMQRQJovQ"
    },
    {
        id: 124,
        name: "Łaskawość Twoja Panie",
        youTubeUrl: null,
        presentationId: "13kuDG0ud0QSPJFJjuTTC-NKRB-2i7anw7d1VMWR42ko"
    },
    {
        id: 286,
        name: "łzaaaaaacde",
        youTubeUrl: null,
        presentationId: "1IQ5YsycMs5dUSMzrNVk8zc2AA95cywpUy7sm4pS6oFs"
    }];
  }
}
