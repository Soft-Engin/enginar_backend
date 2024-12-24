using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.InteractionModels;

namespace BackEngin.Data
{
    public class DataContext : IdentityDbContext<IdentityUser>
    {
        private static readonly byte[][] PreGeneratedImages = new byte[10][]
        {
            Convert.FromBase64String("/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDABQODxIPDRQSEBIXFRQYHjIhHhwcHj0sLiQySUBMS0dARkVQWnNiUFVtVkVGZIhlbXd7gYKBTmCNl4x9lnN+gXz/2wBDARUXFx4aHjshITt8U0ZTfHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHz/wAARCAAtAEYDASIAAhEBAxEB/8QAGgAAAQUBAAAAAAAAAAAAAAAABQABAgMEBv/EACwQAAICAgECBgECBwAAAAAAAAECAxEABBIhMQUTIkFRYXGBoQYjJDJy0fH/xAAZAQADAQEBAAAAAAAAAAAAAAABAwQCAAX/xAAdEQACAgMBAQEAAAAAAAAAAAAAAQIRAxIhMUFR/9oADAMBAAIRAxEAPwDrgckMh7Yu2EBPK3tjQ7ZK8ybWysLBDZYqWAXuQMzOWqsKOY8emlj3XijVmWRQTQJJHbFB4U5hLzj1kdVUWV9+o/asORzqT58ilAFpuVWetAdMlLCszK/Io6EWf7T+PsZJLJ+DFHXoL0uWsgbyKZ2sdvj3yWzoa+5KNks0MnZ1FAMc2rMjTSBoTH5J6kjofu8pOvHTTPy8w+tRRB6C6OLTafDcnGat+mTR3Q0k0GxFyCMeLK1/VYsoj8JM6mWCSIpIeVMe2LKVkVCnjknR2F4142MTlBgkpu8GeORKYo5SWWRDSMPa+9/WEIm/mEfIwV/EwkOtEiAkO1NRrMz8A3QL0NlVRoCyc5HNvXItf5983aomRJBxkchwGLEBmWu+Co2EWxK+ugJBXiK7AijX64Rg3I5U4Nstzrpfc/nIZIe1xMkyCKTy0LBAORMj3be1/jvlibCyIfPlhU0VNMGBb/mB92BV2EEXmbBMYLNy9LH5Jy/RD6olHkxuQbta438HOa+napL3ptTX0tdz6QoYXRPp/TFmSXdWEIZo0DkUxBssf8fbFnayZ29fTqbxjjDHPbPRElbGjY7jLXEexrnmCyEeoDKX7ZQk7QyiuoY0RgasAF2vDNiKaPZWP+njNkGg3H/dZVpPrs0rsQgU8VZ3s0RnYkAiiLBwbs+D60kDRi1Vm5ED5u8nnj5w1HgEn2pGg8jXfzuPRpCo41lEUnmwlGWOOOMhiUvr+/3m2bwmPgyiQqbqwOlk1dYQ8P0oWSnjQlasha5fnERjbofKUNeLpgg8BG2Gn2lKNIbCKaoffTFnQk/HTFlqikqJn30//9k="),
            Convert.FromBase64String("/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDABQODxIPDRQSEBIXFRQYHjIhHhwcHj0sLiQySUBMS0dARkVQWnNiUFVtVkVGZIhlbXd7gYKBTmCNl4x9lnN+gXz/2wBDARUXFx4aHjshITt8U0ZTfHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHz/wAARCABRAH0DASIAAhEBAxEB/8QAGgAAAgMBAQAAAAAAAAAAAAAAAQIAAwUEBv/EACoQAAIDAAEDAwIGAwAAAAAAAAABAgMRBBIhMQVBURMiFDJhcbHRM0Jy/8QAGAEBAQEBAQAAAAAAAAAAAAAAAAECAwT/xAAaEQEBAAMBAQAAAAAAAAAAAAAAAQIRMQMh/9oADAMBAAIRAxEAPwDai8LEylMsiyKtQyK0x0whiA0gBIAgVCEABGKxmxWwFaFaHYrAUIAaBXEsRVEsQFqGTK0x0wH0IiZXyeQuPU5Na/ZfIF7aitk0l8t4U/i+NufiK9/6MDk3W8iXVbJy+F7ISvjStsjBJJy7nO+ki6ehnyqoOP3b1PFhampLU9Rg3cafD6U5/UhLymsw6uLfOuSilKcGt3+zUy2upeNMDBCanHqj4JpplGBhYrYAYAsUCtDplaY0WBamMmIg6A+nB6hs5r4XZHZpTyK+uHUu+PuS8GcqQ1RspmrVDc8ad9UnCnVXut53wEJ2QioShu90/B5q3tn2Tt5Nidj3PCS7I6eNqjOO48Il03Trn+ZPz7PRpV9P3fodPPqxdxZtPpfh/wAnSY1VzqSxN/f9vf3NfTqzkIGEDKyVijMAFKLIlSZZEgsQRUECNiq36cu62L8oLKbPAHZTVCUH0S6629TX+v6BlUofc9lniMVp5/lStpn102Trl8xeHFb656pFOK5c0vnFv8GdRXoLePKtS5F6yUnqjpwcj1GEE1P836HB6fbfdTbdddOy2Us6py14jn5Dc7c8ssknG41OLarrYYuyfV/RrwlqMT02HTFv5eGxX4Kzld1dpNAArKNi6RgAq9yyLK8GRBamERMOgMyuSH0Vgcl9Smnpl8jgqW9jbkiuNXXLGu3uRWfxuA4caEdab19iu7jRphua38m28im8MnlWfVvUV4iVvfxZxY9MUjRrZx0R7HXAMLdA2DQNlQWwAIAq8IJCEDLwEhAqexGQgCSGp9/3IQLC8j8j/Yxq/wDLL9yEC3jQp8I6YkIGTAIQIAUQgH//2Q=="),
            Convert.FromBase64String("/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDABQODxIPDRQSEBIXFRQYHjIhHhwcHj0sLiQySUBMS0dARkVQWnNiUFVtVkVGZIhlbXd7gYKBTmCNl4x9lnN+gXz/2wBDARUXFx4aHjshITt8U0ZTfHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHz/wAARCAAtAEYDASIAAhEBAxEB/8QAGgAAAQUBAAAAAAAAAAAAAAAABQABAgMEBv/EADAQAAIBAwMCBAMIAwAAAAAAAAECAwAEEQUSIRMxIkFRYYGRoQYUIzIzUrHRgsHh/8QAFwEBAQEBAAAAAAAAAAAAAAAAAQACA//EABoRAAMBAQEBAAAAAAAAAAAAAAABERICIQP/2gAMAwEAAhEDEQA/AOwBpwa5ObUb+CUxTTMJVbBCkfCtsFzqDE73ljx++Pv9KKOQ0f1RVgPFA3urtOeozNxx0+ag19fY8Kyn/D/lWiyHyeafPFc2+oXqleoJUUnGWXFE9MkupIme54VsFPUipOk1AjnAqBPemJqLNxWjJW54FKq5G7UqiBpkaaQs0SYbG47xk/St0V4kYBml2oeFyPnzXKJqTsu2MMW9xijrWsr6ZCtw7uGAIRAD7gH+65ttKo2lWETf27SBUmDOewFPNdCPaesmwnOc+VCLMwwzpG0EMc5HiAcqQM9sHuanNbWjmQQ2xjlIPibcoB9e/NYXcXpt/OuIaeS2ui81zdAgj8MHIAGecD4Vqj1BJbWIwvuOAvgU4474rAmh9eIrJqMnVA8LJ+UD3FRtriTRYDG9zHMC+T4skU68qDPsDyyb4kfOC3ODxWae7ZXZAMFTjjzrHFrMl2diBAMZLMcD51kurwxTyPsEqE4DRsD2rXPWlTL5jNzXjA4bB9zSoI+qdR/DCQB6mlTSgtIhSe26hVgQSMsck1OSTWLR2EL7ofJWAI+RonEFijBVQOOwqSO0uSxx7CkKc3PcXL3AmntI+qPPxL/urjqmoOuxUjx7hm/k0c6cYcnprnPoKsXaBwgzxWYjWmBLRL+4mQz3YiRTkIcKPkOK3R/Z6NI5Edt4kbcMLjHtRAAAHKqQSfKqGXClEJRTxtHY/CkKUjTzDBLZRiMoRt3MOc+YH91XBp4tbURE71ySDj+avgvGe4+7ui5HZl4+lK4laM4U4HpSRje2VDkefrSqLXBYlSMY9KVBH//Z"),
            Convert.FromBase64String("/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDABQODxIPDRQSEBIXFRQYHjIhHhwcHj0sLiQySUBMS0dARkVQWnNiUFVtVkVGZIhlbXd7gYKBTmCNl4x9lnN+gXz/2wBDARUXFx4aHjshITt8U0ZTfHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHz/wAARCAAtAEYDASIAAhEBAxEB/8QAGgAAAgMBAQAAAAAAAAAAAAAAAwUAAgQGAf/EACkQAAIBAwMDAwQDAAAAAAAAAAECAwAEEQUSIRMxQSJRYRQjMnFigcH/xAAYAQEAAwEAAAAAAAAAAAAAAAACAAEDBP/EACARAQADAAEEAwEAAAAAAAAAAAEAAhEhAxIxQSIjMlH/2gAMAwEAAhEDEQA/AFdjGJI3B/qjRA20oYfiTyKPpESmBufUTxRpLdpGCquWPAApYNcZn3NbR3AkckIfjGO9Y9V06O6tyYiplTlcEc/FZ5wsel/Ti6jeeM7tinORzxSpLqJLclYwJvDCufknbTp1uKuT1LYBVLEDdTnRrBQHmIz4Fc+bplj6bj5wRTvT9YVIoIGCqvZnPv7/AKptnMYL9MEauxvJbjaeK5HUvXdMo7A12QcTR5jcEHsRyK5S5tpIriXrDByT+6lTmZrhFTLg1KtIMMalKXGOmzoq9Njgk8GmUzyw2Uskakk+ksByqkcmueC4AINOdL1EJHKk78bfSPc0n8w1r9gxS04YqEHq7ZHmisqxSB4FYkfl8H4q1nAk0ztIwiHJHFVkcQTtHAdwYdy2c1jvOE6y1bPy8zx4pboB9mMngnzV7aDY4leRTg4A+aDIZY1wzEgeB4qnTeRTKGUfxzUxSNtT+axvZXwtLx9rN03GSpPn3plrCi604TxYJXnPuPNcojFAdy8txzTZ78S6VFbYIlBH621YJMep2vMVS43VKkvepTmUNaBHJVzwaKkXQuFfG9Ac4rAGI7URJ3HmkPqBHdI7M1hcAlw0DquFI4rJDYQTwNNHc/cUchu1YGuHPtQi5Pc0O0PE1LvubBbTNJ07hxCu3dlz3H+1ezms4IH6kTyTA+lvArAzs+CxJPbk1dWqZI29za+pTzn70aSoOMFRihTTLu3KoXjAUeKzsxoZYmrAIVXzLM+41KHUqSp//9k="),
            Convert.FromBase64String("/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDABALDA4MChAODQ4SERATGCgaGBYWGDEjJR0oOjM9PDkzODdASFxOQERXRTc4UG1RV19iZ2hnPk1xeXBkeFxlZ2P/2wBDARESEhgVGC8aGi9jQjhCY2NjY2NjY2NjY2NjY2NjY2NjY2NjY2NjY2NjY2NjY2NjY2NjY2NjY2NjY2NjY2NjY2P/wAARCAAoABsDASIAAhEBAxEB/8QAGgAAAQUBAAAAAAAAAAAAAAAABgACAwQFAf/EACkQAAIBAwIFBAIDAAAAAAAAAAECAwAREgQhBRMxQVEiYXGRIzKBoeH/xAAYAQADAQEAAAAAAAAAAAAAAAAAAgMBBP/EAB0RAAICAgMBAAAAAAAAAAAAAAABAhEDIRITMVH/2gAMAwEAAhEDEQA/AB4OsSk85TKjAcsH9v5py6ksAWGJ32vT+BFpdeM0jbGLEBQF7/2a3dPLGuoMLcsEglFY2J+f9peyURljUt+A1JLkV37VVLXJPvRHxjWaWXQumluz4gsAD6bEdfehdyUcq2xplk5KzHCtFvQBpGdV39IPQeaJTr3mSYS8s5riCU2tv9+fFC3C5EXVfkHpK4k3tj71trxBoFYg6exW2eOIPx5rnyXstFxpKrZzjE+mh0nI0ShRbNwym5sR579b1RSHQyIrsZLkDuot9mpuJqicPWaOVXeYq1779qw5A2bZlsr73qmGSUfpHIm36MDMu6sR22rodxYB2AHTfpSpU9BZPlFHOrwM0m4OMg2PzUcuoMkrOI41ub2A2FKlQkEnej//2Q=="),
            Convert.FromBase64String("/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDABALDA4MChAODQ4SERATGCgaGBYWGDEjJR0oOjM9PDkzODdASFxOQERXRTc4UG1RV19iZ2hnPk1xeXBkeFxlZ2P/2wBDARESEhgVGC8aGi9jQjhCY2NjY2NjY2NjY2NjY2NjY2NjY2NjY2NjY2NjY2NjY2NjY2NjY2NjY2NjY2NjY2NjY2P/wAARCAAbABYDASIAAhEBAxEB/8QAGgAAAgIDAAAAAAAAAAAAAAAAAAUDBAECBv/EACcQAAIBBAECBgMBAAAAAAAAAAECAwAEESESBTETQVFxsfEiJJHR/8QAFgEBAQEAAAAAAAAAAAAAAAAAAwAE/8QAHREAAgEEAwAAAAAAAAAAAAAAAAECAxESMjFRYf/aAAwDAQACEQMRAD8AqdT6RCsUPhckKEMylizA58gdfVI722ZI+U8gEuOxI9+wpvf9SmS7hBEUiOAx4jGCDis+FHK6rcxK0szcTJg7GNd/Ohcm7W4KMVFtMQyCHgrwpnlnP4ZxRW9wgt7mS2Z34KSRw3sHH8op1TLPw6q4sYXt3QeGqkEHjr6pTcx3ShmaZEQEhpFXYHqceut1PJNI9uxZiTj/AGo4ZHaKZWOQMYHud/FBS3sxZ65IrPb/ALLvaEc8AOz7GcA6+aKVvPKp4iRwB2ANFaVVXQLgz//Z"),
            Convert.FromBase64String("/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDABQODxIPDRQSEBIXFRQYHjIhHhwcHj0sLiQySUBMS0dARkVQWnNiUFVtVkVGZIhlbXd7gYKBTmCNl4x9lnN+gXz/2wBDARUXFx4aHjshITt8U0ZTfHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHz/wAARCAAfABYDASIAAhEBAxEB/8QAGQAAAgMBAAAAAAAAAAAAAAAAAAIDBAUG/8QAJRAAAgEDBAICAwEAAAAAAAAAAQIDABESBAUTITFBMqEUIlFh/8QAFgEBAQEAAAAAAAAAAAAAAAAAAgAD/8QAGhEAAgMBAQAAAAAAAAAAAAAAAAECESExEv/aAAwDAQACEQMRAD8A53TcTBxOWQ+j35p9v1XHKvJdlU9C9vulk1MmqWKAJGhQfIdX6+q0xs+O3/kZNygZEeeqy9U9E+YIIDq9RJxZuCS1yO/Q9UVRBfTuXLSI3xOB7orRMOFFrxy3UjzcVsy79LNp1QkKF8AAdn/azGhHHle9v7VuDb4m206vkbIAnHHq4NGST6UbawhllZ4g0pLMx+vVFJMMYlsf2J7opEf/2Q=="),
            Convert.FromBase64String("/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDABQODxIPDRQSEBIXFRQYHjIhHhwcHj0sLiQySUBMS0dARkVQWnNiUFVtVkVGZIhlbXd7gYKBTmCNl4x9lnN+gXz/2wBDARUXFx4aHjshITt8U0ZTfHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHz/wAARCAALAA8DASIAAhEBAxEB/8QAFgABAQEAAAAAAAAAAAAAAAAABAID/8QAIxAAAgICAQIHAAAAAAAAAAAAAQIDBAARMQUhEhMiQVGRwf/EABQBAQAAAAAAAAAAAAAAAAAAAAL/xAAWEQADAAAAAAAAAAAAAAAAAAAAESH/2gAMAwEAAhEDEQA/AF2LNuvAoqK3jJ9RYe2z2G/vKrTT9QllilaRPKPY60WHH5mCzyzrZeV2ZkKhTxrn4x/TGJqlySX3rZwKjcP/2Q=="),
            Convert.FromBase64String("/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDABQODxIPDRQSEBIXFRQYHjIhHhwcHj0sLiQySUBMS0dARkVQWnNiUFVtVkVGZIhlbXd7gYKBTmCNl4x9lnN+gXz/2wBDARUXFx4aHjshITt8U0ZTfHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHz/wAARCAAJAA8DASIAAhEBAxEB/8QAFQABAQAAAAAAAAAAAAAAAAAABAP/xAAkEAACAQIDCQAAAAAAAAAAAAABAwIAEgQRMwUhMVFxgYOx0f/EABUBAQEAAAAAAAAAAAAAAAAAAAME/8QAGBEBAQADAAAAAAAAAAAAAAAAAQACESH/2gAMAwEAAhEDEQA/AITxKYvWV2iIWY2kZm7ln3pSFoYoE4eNsSRESB3cKDs/Vb5fQpz9E9flTvJDEXV//9k="),
            Convert.FromBase64String("/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDABQODxIPDRQSEBIXFRQYHjIhHhwcHj0sLiQySUBMS0dARkVQWnNiUFVtVkVGZIhlbXd7gYKBTmCNl4x9lnN+gXz/2wBDARUXFx4aHjshITt8U0ZTfHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHx8fHz/wAARCAAPAA8DASIAAhEBAxEB/8QAFgABAQEAAAAAAAAAAAAAAAAABAEF/8QAHhAAAgICAgMAAAAAAAAAAAAAAQIDEQASITIEQWH/xAAUAQEAAAAAAAAAAAAAAAAAAAAD/8QAFxEAAwEAAAAAAAAAAAAAAAAAAAESEf/aAAwDAQACEQMRAD8AIvipLKDJP27ELePZoo3WJJCbW1OvBGZRnAQgKQxrn5lSVRJuVYj0LqsNUI5w/9k=")

        };

        private static readonly byte[][] IngredientImages = new byte[2][]
        {
            Convert.FromBase64String("/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDAFA3PEY8MlBGQUZaVVBfeMiCeG5uePWvuZHI////////////////////////////////////////////////////2wBDAVVaWnhpeOuCguv/////////////////////////////////////////////////////////////////////////wAARCABAAEADASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwCT73QimsOcdagVsHg1ajORk1IyIxk9qZ5RIz2qWaXHAqNfMl4HSmBEVpCtWvIwOopjRgdxSuBXxSYqYp3NNK07gP8A3a9Fz9TS+e2MAAVBu9KsWyAZlft0oAekGfnl/KpSwUccDFN3EtuIIGKhkkyT1qR2HPJURYmm9TSjoc0DFQkHB6GnMOeKaen1qQ/dH0piIESrBO2NRnFMQUsqkqCO3WhghC7MuSeKizmlJwgUU2gY8dMGjOOKZmkLUCJEG9gBU7VVSRl6VIWYjmiwCqalDdhUANPDYoENm4PAqHJqwSCKjZRTAi5NLilxUkUW75m+6P1oAWGLje3TtQ7c8U+SQYwDUJOaAP/Z"),
            Convert.FromBase64String("/9j/4AAQSkZJRgABAQAAAQABAAD/2wBDAFA3PEY8MlBGQUZaVVBfeMiCeG5uePWvuZHI////////////////////////////////////////////////////2wBDAVVaWnhpeOuCguv/////////////////////////////////////////////////////////////////////////wAARCABAAEADASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwCsKcKQU4UDJRHkcOp9s0gpSAwBH40eWEPXg9Km4gopxGOKQcj0pjGEU0ipCKaRQBEKcKaKUUwJFpz5YADqRxSBTkAc56VMIj8hOODk1NrgiJCGC9m9+9SbTtz6VG3yvtb14pshIyFb8KOtirDzTTSq24Z796DTJK4pwpgqSMkMB68c0wLcKlYwT1NO3D14pCRwKQcYxSER3BA296rMcsTUsrbnwOwqEZoL8iSM7eT0NPJyKj4wM9qec0CZY8iMdqDEuMgfSq4uCevSpBc560CFLFRyCPWoZJSeADUxmVuucfWmbYSc4IP1oHcg2nknj60mCBmrBSMjhm/Ok8qI87jQFxYYQPmdhn0zUxVfUVB5UXqaNkY9aBH/2Q==")
        };


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<Roles> Roles { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Preferences> Preferences { get; set; }
        public DbSet<IngredientTypes> IngredientTypes { get; set; }
        public DbSet<Ingredients> Ingredients { get; set; }
        public DbSet<Recipes> Recipes { get; set; }
        public DbSet<Recipes_Ingredients> Recipes_Ingredients { get; set; }
        public DbSet<Blogs> Blogs { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<Addresses> Addresses { get; set; }
        public DbSet<Districts> Districts { get; set; }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<Countries> Countries { get; set; }
        public DbSet<Events_Requirements> Events_Requirements { get; set; }
        public DbSet<Requirements> Requirements { get; set; }


        public DbSet<Users_Interactions> Users_Interactions { get; set; }
        public DbSet<Interactions> Interactions { get; set; }
        public DbSet<Users_Recipes_Interaction> Users_Recipes_Interactions { get; set; }
        public DbSet<Users_Blogs_Interaction> Users_Blogs_Interactions { get; set; }
        public DbSet<Ingredients_Preferences> Ingredients_Preferences { get; set; }
        public DbSet<User_Event_Participations> User_Event_Participations { get; set; }

        public DbSet<Blog_Bookmarks> Blog_Bookmarks { get; set; }
        public DbSet<Blog_Comments> Blog_Comments { get; set; }
        public DbSet<Blog_Likes> Blog_Likes { get; set; }
        public DbSet<Recipe_Bookmarks> Recipe_Bookmarks { get; set; }
        public DbSet<Recipe_Comments> Recipe_Comments { get; set; }
        public DbSet<Recipe_Likes> Recipe_Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Countries>().HasData(
               new Countries { Id = 1, Name = "Turkey" },
               new Countries { Id = 2, Name = "USA" }
           );

            modelBuilder.Entity<Cities>().HasData(
                new Cities { Id = 1, Name = "Istanbul", CountryId = 1 },
                new Cities { Id = 2, Name = "New York", CountryId = 2 }
            );

            modelBuilder.Entity<Districts>().HasData(
                new Districts { Id = 1, Name = "Kadikoy", CityId = 1, PostCode = 34710 },
                new Districts { Id = 2, Name = "Besiktas", CityId = 1, PostCode = 34353 },
                new Districts { Id = 3, Name = "Rat Street", CityId = 2, PostCode = 42069 }
            );

            modelBuilder.Entity<Addresses>().HasData(
                new Addresses { Id = 1, Name = "Office Address", DistrictId = 1, Street = "Main Avenue" },
                new Addresses { Id = 2, Name = "Home Address", DistrictId = 2, Street = "Second Street" }
            );

            modelBuilder.Entity<Events>().HasData(
                new Events
                {
                    Id = 1,
                    CreatorId = "1",
                    AddressId = 1,
                    Date = new DateTime(2024, 12, 31, 0, 0, 0, DateTimeKind.Utc),
                    Title = "New Year's Eve Party",
                    BodyText = "Celebrate the New Year with us!"
                }
            );

            modelBuilder.Entity<User_Event_Participations>().HasData(
                new User_Event_Participations
                {
                    Id = 1,
                    UserId = "1",
                    EventId = 1
                }
            );

            modelBuilder.Entity<Requirements>().HasData(
                new Requirements { Id = 1, Name = "RSVP Required", Description = "Guests must confirm attendance before the event." },
                new Requirements { Id = 2, Name = "Dress Code", Description = "Guests are required to follow the formal dress code." },
                new Requirements { Id = 3, Name = "Age Limit", Description = "Only guests aged 18 and above are allowed to attend." }
            );

            modelBuilder.Entity<Events_Requirements>().HasData(
                new Events_Requirements { Id = 1, EventId = 1, RequirementId = 1 }, // "RSVP Required" for the "New Year's Eve Party"
                new Events_Requirements { Id = 2, EventId = 1, RequirementId = 2 }, // "Dress Code" for the "New Year's Eve Party"
                new Events_Requirements { Id = 3, EventId = 1, RequirementId = 3 }  // "Age Limit" for the "New Year's Eve Party"
            );


            modelBuilder.Entity<Roles>().HasData(
                new Roles { Id = 1, Name = "User", Description = "Default user role" },
                new Roles { Id = 2, Name = "Admin", Description = "Admin role" }
            );

            modelBuilder.Entity<Ingredients>().HasData(
                new Ingredients { Id = 3, Name = "Enginar", TypeId = 1 },
                new Ingredients { Id = 4, Name = "Zeytinyağı", TypeId = 2 }
            );

            modelBuilder.Entity<Users>().HasData(
                new Users { Id = "1", FirstName = "Engin", LastName = "Adam", UserName = "EnginarAdam",  RoleId = 1 },
                new Users { Id = "2", FirstName = "Engin", LastName = "Kadın", UserName = "EnginarKadın", RoleId = 1 },
                new Users { Id = "3", FirstName = "Engin", LastName = "Çocuk", UserName = "EnginarÇocuk", RoleId = 1 },
                new Users { Id = "4", FirstName = "Engin", LastName = "Yaşlı", UserName = "EnginarYaşlı", RoleId = 1 },
                new Users { Id = "5", FirstName = "Engin", LastName = "Enginar", UserName = "EnginarDouble", RoleId = 2 }
            );

            modelBuilder.Entity<Recipes>().HasData(
                new Recipes { 
                    Id = 2, 
                    Header = "Enginar Şöleni", 
                    BodyText = "Enginarları küp küp doğra zeytin yağında kavur zart zrut",
                    ServingSize = 2, 
                    PreparationTime = 45,  
                    UserId = "1", 
                    CreatedAt = new DateTime(),
                    BannerImage = GetDummyImage(),
                    StepImages = GenerateStepImages(3), // Generate 3 step images
                    
                    Steps = new[] { 
                        "Enginarları temizle", 
                        "Zeytinyağında kavur", 
                        "Servis et" 
                    }
                }
            );

            modelBuilder.Entity<Blogs>().HasData(
                new Blogs { Id = 1, RecipeId = 2, Header = "ENGINAR YOLCULUĞU", BodyText = "benimle enginarın sırlarını keşfetmeye yelken açın", UserId = "1" , CreatedAt = new DateTime()}
            );
            // Configure many-to-many relationship between Ingredients and Preferences
            modelBuilder.Entity<Ingredients_Preferences>()
                .HasOne(ip => ip.Ingredient)
                .WithMany(i => i.Ingredients_Preferences)
                .HasForeignKey(ip => ip.IngredientId);

            modelBuilder.Entity<Ingredients_Preferences>()
                .HasOne(ip => ip.Preference)
                .WithMany(p => p.Ingredients_Preferences)
                .HasForeignKey(ip => ip.PreferenceId);

            // Users Table
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasOne(u => u.Address)
                    .WithMany()
                    .HasForeignKey(u => u.AddressId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(u => u.Role)
                    .WithMany()
                    .HasForeignKey(u => u.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Events Table
            modelBuilder.Entity<Events>(entity =>
            {
                // Ensure Date is stored as 'timestamp with time zone' in PostgreSQL
                entity.Property(e => e.Date).HasColumnType("timestamptz");
                entity.Property(e => e.CreatedAt).HasColumnType("timestamptz");

                // Apply value converter for Date and CreatedAt to convert to UTC automatically
                entity.Property(e => e.Date)
                    .HasConversion(
                        v => v.ToUniversalTime(),  // Convert to UTC before saving
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc) // Ensure it's UTC on reading
                    );

                entity.Property(e => e.CreatedAt)
                    .HasConversion(
                        v => v.ToUniversalTime(),  // Convert to UTC before saving
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc) // Ensure it's UTC on reading
                    );

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(e => e.Creator)
                    .WithMany()
                    .HasForeignKey(e => e.CreatorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Address)
                    .WithMany()
                    .HasForeignKey(e => e.AddressId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // User_Event_Participations Table
            modelBuilder.Entity<User_Event_Participations>(entity =>
            {
                entity.HasKey(uep => uep.Id);

                entity.HasOne(uep => uep.User)
                    .WithMany()
                    .HasForeignKey(uep => uep.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(uep => uep.Event)
                    .WithMany()
                    .HasForeignKey(uep => uep.EventId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Addresses Table
            modelBuilder.Entity<Addresses>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.HasOne(a => a.District)
                    .WithMany()
                    .HasForeignKey(a => a.DistrictId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            PopulatePreferences(modelBuilder);
            ConfigureUserInteractions(modelBuilder);
            PopulateIngredientTypes(modelBuilder);
            PopulateMockData(modelBuilder);
        }

        private byte[] GetDummyImage()
        {
            var randomIndex = Random.Shared.Next(PreGeneratedImages.Length);
            return PreGeneratedImages[randomIndex];
        }

        private byte[][] GenerateStepImages(int count)
        {
            var images = new byte[count][];
            for (int i = 0; i < count; i++)
            {
                images[i] = GetDummyImage();
            }
            return images;
        }

        private void PopulateMockData(ModelBuilder modelBuilder)
        {

            // Add more ingredients
            // Existing: Id=3 (Enginar), Id=4 (Zeytinyağı)
            // Add a variety of other ingredients
            modelBuilder.Entity<Ingredients>().HasData(
                new Ingredients { Id = 1, Name = "Patates", TypeId = 1 , Image = IngredientImages[0] },
                new Ingredients { Id = 2, Name = "Ejder Meyvesi", TypeId = 2 , Image = IngredientImages[1] },
                new Ingredients { Id = 5, Name = "Domates", TypeId = 1 },
                new Ingredients { Id = 6, Name = "Soğan", TypeId = 1 },
                new Ingredients { Id = 7, Name = "Sarımsak", TypeId = 1 },
                new Ingredients { Id = 8, Name = "Tuz", TypeId = 7 },        // Spice
                new Ingredients { Id = 9, Name = "Biber", TypeId = 7 },      // Spice
                new Ingredients { Id = 10, Name = "Limon Suyu", TypeId = 2 },
                new Ingredients { Id = 11, Name = "Marul", TypeId = 1 },
                new Ingredients { Id = 12, Name = "Peynir", TypeId = 4 },     // Dairy
                new Ingredients { Id = 13, Name = "Bulgur", TypeId = 5 },     // Grain
                new Ingredients { Id = 14, Name = "Balık", TypeId = 6 },      // Seafood
                new Ingredients { Id = 15, Name = "Yumurta", TypeId = 6 },    // Let's assume seafood = animal products, or add a new type if needed
                new Ingredients { Id = 16, Name = "Maydanoz", TypeId = 8 },   // Herb
                new Ingredients { Id = 17, Name = "Ceviz", TypeId = 9 },       // Nuts & Seeds
                new Ingredients { Id = 18, Name = "Nane", TypeId = 8 },        // Herb
                new Ingredients { Id = 19, Name = "Elma", TypeId = 2 },        // Fruit
                new Ingredients { Id = 20, Name = "Bal", TypeId = 2 }          // Fruit/honey (assuming Fruit type)
            );


            modelBuilder.Entity<Recipes_Ingredients>().HasData(
                new Recipes_Ingredients { Id = 3, RecipeId = 2, IngredientId = 3, Quantity = 2, Unit = "adet" },          // Enginar
                new Recipes_Ingredients { Id = 4, RecipeId = 2, IngredientId = 4, Quantity = 3, Unit = "yemek kaşığı" }  // Zeytinyağı
            );



            // Additional Recipes (20 total, including Id=2)
            var recipesToAdd = new List<Recipes>();
            var recipeHeaders = new[]
            {
        "Zeytinyağlı Enginar Kulesi", "Enginar & Zeytinli Püre", "Bahar Enginar Salatası", "Enginar Çorbası",
        "Enginar Soslu Makarna", "Enginar & Avokado Salatası", "Zeytinyağlı Enginar Ruloları", "Enginarlı Yoğurtlu Meze",
        "Enginar Dolgulu Kabak Çiçeği", "Enginarlı Humus", "Kremalı Enginar Çorbası", "Enginar Pane",
        "Enginar Frittata", "Enginar & Fesleğenli Pesto", "Enginarlı Pizza", "Enginar Kebabı",
        "Baharatlı Enginar Atıştırmalığı", "Enginar & Kuşkonmaz Garnitürü", "Enginarlı Patates Püresi", "Limonlu Enginar Tatlısı",
        "Enginar ve Tulum Peynirli Salata", "Fırında Parmesanlı Enginar", "Zeytin Ezmesi ile Enginar Kanepesi",
        "Enginarlı Couscous Salatası", "Kinoa ve Enginar Pilavı", "Enginarlı Karides Sote", "Enginar & Nar Ekşili Sos",
        "Fırında Baharatlı Enginar Yaprakları", "Enginarlı Sebze Güveci", "Enginar Bruschetta", "Zeytinyağlı Enginar Şakşuka",
        "Enginarlı Mercimek Salatası", "Enginar Tatlısı", "Enginar ve Somon Carpaccio", "Enginar Tartar",
        "Enginar Püresi ile Dana Eti", "Enginar & Ispanaklı Pide", "Enginarlı Kabak Çorbası", "Enginar Çiçeği Tatlısı",
        "Enginarlı Zeytinyağlı Sarma", "Fırında Enginarlı Kabak", "Enginar ve Labneli Tart", "Enginarlı Bezelye Garnitürü",
        "Enginar Dolgulu Tavuk", "Enginarlı Makarna Salatası", "Fırında Enginarlı Yumurta", "Enginar Graten",
        "Enginar ve Hardal Sosu", "Enginarlı Deniz Mahsulleri Tabağı", "Enginar Çıtırları", "Enginar Kalbi Mezesı",
        "Zeytinyağlı Enginar Kulesi", "Enginar & Zeytinli Püre", "Bahar Enginar Salatası", "Enginar Çorbası",
        "Akdeniz Enginar Tabağı", "Limonlu Enginar Sosu", "Fırında Enginar Cipsi", "Enginar & Peynir Ezmesi",
        "Enginarlı Bulgur Pilavı", "Zeytinyağlı Enginar Dolması", "Közlenmiş Enginar Kreması", "Enginar Turşusu",
        "Enginarlı Yeşil Salata", "Enginarlı Omlet", "Enginar Köftesi", "Enginar Smoothie",
        "Enginar Tart", "Enginar Çayı", "Enginar Suflesi", "Enginar Patatesli Güveç",
        "Enginar ve Tereyağlı Sos", "Enginarlı Dondurma", "Enginarlı Beyaz Peynir Ezmesi", "Enginar Kroket",
        "Enginar & Fırında Kuşkonmaz", "Enginar Tatlı Topları", "Enginar Fırında Peynirli", "Enginar Çikolata Fonu",
        "Enginar ve Yoğurtlu Kabak", "Enginar Çorbası Parmesanlı", "Enginar Çorbası Tavuklu", "Enginar Kalbi Pane",
        "Zeytinli Enginar Tart", "Enginar ve Baharatlı Sebzeler", "Enginar Şiş", "Enginar Zeytinyağlı Pilaki",
        "Enginarlı Mantı", "Enginar Tavuklu Salata", "Enginarlı Lahana Salatası", "Enginar ve Fesleğenli Frittata",
        "Enginar Tava", "Enginar Çubukları", "Enginar & Maydanozlu Tereyağı", "Fırında Enginar Sufle",
        "Enginar & Limonlu Patates", "Enginar Böreği", "Enginarlı Havuçlu Salata", "Enginar Tatlı Soslu",
        "Enginar Ezmesi Pesto", "Enginarlı Balık Güveç", "Enginar ve Naneli Dip", "Enginar Kalp Tabağı", "Oğuzhan'ın Enginar Suflesi",
    };

            int recipeIdCounter = 3;
            Random rand = new Random();
            foreach (var header in recipeHeaders)
            {
                // Random userId from "1" to "5"
                string userId = rand.Next(1, 6).ToString();
                recipesToAdd.Add(new Recipes
                {
                    Id = recipeIdCounter,
                    Header = header,
                    BodyText = $"{header} hazırlanışı, enginar ve çeşitli malzemelerle harmanlanır.",
                    UserId = userId,
                    ServingSize = rand.Next(1,11),
                    PreparationTime = rand.Next(1,25) * 15,
                    CreatedAt = new DateTime(),
                    BannerImage = GetDummyImage(),
                    StepImages = GenerateStepImages(3), // Generate 3 step images
                    Steps = new[] { 
                        "Malzemeleri hazırla", 
                        "Karıştır ve pişir", 
                        "Servis et" 
                    }
                });
                recipeIdCounter++;
            }

            modelBuilder.Entity<Recipes>().HasData(recipesToAdd);

            var recipeIngredientsToAdd = new List<Recipes_Ingredients>();

            // Add Recipes_Ingredients for new Recipes
            // Assign a random set of 2-3 ingredients for variety
            var allIngredientsIds = new[] {1,2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
            var measuremenetsUnits = new[] { "adet", "kap", "bardak", "tane", "çimdik", "avuç", "yemek kaşığı", "tatlı kaşığı", "kilo" };
            // We'll keep it simple and just pick 3 random ingredients per recipe.
            int riCounter = 5;
            for (int rId = 3; rId <= 2 + recipeHeaders.Length; rId++)
            {
                // pick 3 distinct ingredients randomly
                var ingSet = allIngredientsIds.OrderBy(x => rand.Next()).Take(rand.Next(3,10)).ToList();
                foreach (var ing in ingSet)
                {
                    recipeIngredientsToAdd.Add(new Recipes_Ingredients
                    {
                        Id = riCounter,
                        RecipeId = rId,
                        IngredientId = ing,
                        Quantity = rand.Next(1, 5),
                        Unit = measuremenetsUnits.OrderBy(x => rand.Next()).First()
                    });
                    riCounter++;
                }
            }

            modelBuilder.Entity<Recipes_Ingredients>().HasData(recipeIngredientsToAdd);

            // Additional Blogs 100 total
            var blogsToAdd = new List<Blogs>();
            var blogHeaders = new[]
                {
                    "Makarna ve Enginar Aşkı", "Avokado ile Hafiflik", "Rulo Şıklığı", "Yoğurtlu Lezzet Patlaması",
                    "Kabak Çiçeğinde Enginar", "Humusun Sırrı", "Kremanın Yumuşaklığı", "Kızarmış Keyif",
                    "Frittata ile Güne Başla", "Fesleğenli Taze Notalar", "Pizzada Akdeniz Esintisi", "Kebabın Enginar Hali",
                    "Baharatlı Atıştırmalıklar", "Kuşkonmazın Tazeliği", "Patates ve Enginar Uyumu", "Limonlu Tatlı Dokunuş",
                    "Tulum Peynirinin Şıklığı", "Parmesan İle Fırın Keyfi", "Zeytinli Kanepenin Lezzeti",
                    "Couscous Şöleni", "Kinoanın Gücü", "Deniz Mahsulleri ve Enginar", "Nar Ekşisinin Aroması",
                    "Yaprakların Fırında Dansı", "Sebze Güvecin Kraliçesi", "Bruschetta ile Hafiflik", "Şakşukanın Yeni Hali",
                    "Mercimek ve Enginar Uyumu", "Tatlıda Enginar Denemesi", "Somonun Zenginliği", "Tartarın Zerafeti",
                    "Dana Etinin Püresi", "Pideye Yeni Bir Soluk", "Kabak ve Enginar Çorbası", "Tatlı Çiçekler",
                    "Zeytinyağlı Dolmanın Kraliçesi", "Kabak Fırında Yeniden", "Labne İle Uyum", "Bezelye ile Taze Bir Dokunuş",
                    "Tavuğun Dolgulu Şöleni", "Makarna Salatasında Fark", "Yumurta ve Fırında Uyumu", "Gratenin Altın Çağı",
                    "Hardallı Sos Şıklığı", "Denizden Sofraya", "Çıtırların Cazibesi", "Kalplerde Bir Lezzet",
                    "Kulenin Zirvesi", "Pürede Gizli Tatlar", "Baharın Getirdiği Tazelik", "Kaseye Dolu Çorba",
                    "Akdeniz Mutfağının İncisi", "Sosun Asidik Tadına Yolculuk", "Fırından Gelen Cips Keyfi", "Peynir Ezmesi ve Lezzet",
                    "Bulgurun Dansı", "Dolmada Şıklık", "Kremanın Közle Uyumu", "Turşuda Keskin Tatlar",
                    "Yeşilin Ferahlığı", "Güne Omletle Başlayın", "Köfte ve Enginarın Uyumu", "Smoothie ile Sağlık",
                    "Tartta Tatlı Kaçamak", "Çayda Farklı Bir Deneyim", "Suflede Zirve Tat", "Güveçte Lezzet Patlaması",
                    "Tereyağında Mucize", "Dondurmanın Enginarlı Hali", "Peynir Ezmesinin Tatlı Versiyonu", "Kroket ile Atıştırmalık Keyfi",
                    "Fırında Kuşkonmaz Uyumu", "Tatlı Topların Zirvesi", "Peynirli Fırında Harmoni", "Çikolatada Enginar Şaşkınlığı",
                    "Yoğurt ve Kabak Uyumu", "Çorbanın Parmesan Dokunuşu", "Tavuklu Çorbanın Sırrı", "Pane ile Altın Renkler",
                    "Tartta Zeytin Aşkı", "Sebzelerin Baharatlı Dansı", "Şiş Lezzet", "Pilakide Hafiflik",
                    "Mantının Enginar Yorumu", "Salatada Tavuğun Efsanesi", "Lahananın Tazeliği", "Frittata Şıklığı",
                    "Tavada Lezzet Patlaması", "Çubuklarda Hafiflik", "Maydanozla Aromalı Tereyağı", "Fırında Sufle Denemesi",
                    "Limonun Enginarla Uyumu", "Börek ve Enginar Şıklığı", "Havuçlu Salatada Farklılık", "Tatlı Sosun Enginar Yorumu",
                    "Ezmede Pesto Esintisi", "Balık Güveçte Tat", "Naneli Dip Ferahlığı", "Kalpten Gelen Lezzet",  "Oğuzhan'ınkini yemeden ben en iyisini yedim deme!"
                };


            int blogIdCounter = 2;
            int recipeCount = 2 + recipeHeaders.Length; // total recipes
            foreach (var bHeader in blogHeaders)
            {
                string userId = rand.Next(1, 6).ToString();
                int recipeId = rand.Next(2, recipeCount + 1); // pick a random recipe from 2 to (2+length)
                blogsToAdd.Add(new Blogs
                {
                    Id = blogIdCounter,
                    RecipeId = recipeId,
                    Header = bHeader,
                    BodyText = $"{bHeader} blog yazısı, enginarın farklı yönlerini keşfedin.",
                    UserId = userId,
                    CreatedAt = new DateTime(),
                    BannerImage = GetDummyImage()
                });
                blogIdCounter++;
            }

            modelBuilder.Entity<Blogs>().HasData(blogsToAdd);
        }

            private void PopulatePreferences(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Preferences>(entity =>
            {
                entity.HasKey(a => a.Id); // Primary key
                entity.Property(a => a.Id).ValueGeneratedOnAdd(); // Auto-increment
            });

            modelBuilder.Entity<Preferences>().HasData(
                new Preferences { Id = 1, Name = "Gluten", Description = "A type of protein commonly found in wheat, barley, and rye." },
                new Preferences { Id = 2, Name = "Dairy", Description = "Milk and products derived from milk, such as cheese and yogurt." },
                new Preferences { Id = 3, Name = "Nuts", Description = "Tree nuts including almonds, cashews, and walnuts; excludes peanuts." },
                new Preferences { Id = 4, Name = "Peanuts", Description = "A type of legume that is a common allergen, distinct from tree nuts." },
                new Preferences { Id = 5, Name = "Soy", Description = "A legume used in products like tofu, soy milk, and many processed foods." },
                new Preferences { Id = 6, Name = "Eggs", Description = "A common ingredient in baking and cooking derived from chicken eggs." },
                new Preferences { Id = 7, Name = "Fish", Description = "Seafood including cod, salmon, and tuna." },
                new Preferences { Id = 8, Name = "Shellfish", Description = "Crustaceans and mollusks like shrimp, crab, and clams." },
                new Preferences { Id = 9, Name = "Sesame", Description = "Seeds and oils derived from sesame plants, found in many cuisines." },
                new Preferences { Id = 10, Name = "Vegan", Description = "A diet that excludes all animal products, including meat, dairy, and honey." },
                new Preferences { Id = 11, Name = "Vegetarian", Description = "A diet that excludes meat and fish but may include dairy and eggs." },
                new Preferences { Id = 12, Name = "Lactose Intolerant", Description = "Avoidance of dairy products due to difficulty digesting lactose." },
                new Preferences { Id = 13, Name = "Pescatarian", Description = "A diet that includes fish but excludes other forms of meat." },
                new Preferences { Id = 14, Name = "Halal", Description = "Dietary requirements based on Islamic law, including avoidance of pork and alcohol." },
                new Preferences { Id = 15, Name = "Kosher", Description = "Food prepared in compliance with Jewish dietary laws, avoiding non-kosher animals and mixing meat with dairy." },
                new Preferences { Id = 16, Name = "Low FODMAP", Description = "A diet that limits fermentable oligosaccharides, disaccharides, monosaccharides, and polyols to manage digestive symptoms." },
                new Preferences { Id = 17, Name = "Nut-Free", Description = "Avoidance of all nuts, including peanuts and tree nuts." },
                new Preferences { Id = 18, Name = "Plant-Based", Description = "A diet primarily focused on consuming plant-derived foods, minimizing or excluding animal products." },
                new Preferences { Id = 19, Name = "Keto", Description = "A low-carb, high-fat diet focused on inducing ketosis for energy." },
                new Preferences { Id = 20, Name = "Paleo", Description = "A diet based on the presumed eating patterns of ancient humans, focusing on whole, unprocessed foods." }
           );
        }

        private void ConfigureUserInteractions(ModelBuilder modelBuilder)
        {
            // Seed Interactions
            modelBuilder.Entity<Interactions>().HasData(
                new Interactions { Id = 1, Name = "Follow", Description = "User follows another user" },
                new Interactions { Id = 2, Name = "BookmarkRecipe", Description = "User bookmarks a recipe" },
                new Interactions { Id = 3, Name = "BookmarkBlog", Description = "User bookmarks a blog" }
            );

            // Configure Users_Interactions Relationships
            modelBuilder.Entity<Users_Interactions>()
                .HasOne(ui => ui.InitiatorUser)
                .WithMany()
                .HasForeignKey(ui => ui.InitiatorUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Users_Interactions>()
                .HasOne(ui => ui.TargetUser)
                .WithMany()
                .HasForeignKey(ui => ui.TargetUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Users_Interactions>()
                .HasOne(ui => ui.Interaction)
                .WithMany()
                .HasForeignKey(ui => ui.InteractionId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Users_Recipes_Interaction>()
               .HasOne(uri => uri.User)
               .WithMany()
               .HasForeignKey(uri => uri.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Users_Recipes_Interaction>()
                .HasOne(uri => uri.Recipe)
                .WithMany()
                .HasForeignKey(uri => uri.RecipeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Users_Recipes_Interaction>()
                .HasOne(uri => uri.Interaction)
                .WithMany()
                .HasForeignKey(uri => uri.InteractionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Users_Blogs_Interaction>()
                .HasOne(ubi => ubi.User)
                .WithMany()
                .HasForeignKey(ubi => ubi.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Users_Blogs_Interaction>()
                .HasOne(ubi => ubi.Blog)
                .WithMany()
                .HasForeignKey(ubi => ubi.BlogId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Users_Blogs_Interaction>()
                .HasOne(ubi => ubi.Interaction)
                .WithMany()
                .HasForeignKey(ubi => ubi.InteractionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Recipe_Bookmarks>()
               .HasOne(rb => rb.Recipe)
               .WithMany()
               .HasForeignKey(rb => rb.RecipeId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Recipe_Comments>()
               .HasOne(rb => rb.Recipe)
               .WithMany()
               .HasForeignKey(rb => rb.RecipeId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Recipe_Likes>()
               .HasOne(rb => rb.Recipe)
               .WithMany()
               .HasForeignKey(rb => rb.RecipeId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Blog_Bookmarks>()
              .HasOne(rb => rb.Blog)
              .WithMany()
              .HasForeignKey(rb => rb.BlogId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Blog_Comments>()
               .HasOne(rb => rb.Blog)
               .WithMany()
               .HasForeignKey(rb => rb.BlogId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Blog_Likes>()
               .HasOne(rb => rb.Blog)
               .WithMany()
               .HasForeignKey(rb => rb.BlogId)
               .OnDelete(DeleteBehavior.Restrict);
        }

        private void PopulateIngredientTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IngredientTypes>(entity =>
            {
                entity.HasKey(it => it.Id); // Primary key
                entity.Property(it => it.Id).ValueGeneratedOnAdd(); // Auto-increment
            });

            modelBuilder.Entity<IngredientTypes>().HasData(
                new IngredientTypes { Id = 1, Name = "Vegetable", Description = "Edible plants or their parts, intended for cooking or eating raw." },
                new IngredientTypes { Id = 2, Name = "Fruit", Description = "Sweet or savory product of a plant that contains seeds and can be eaten as food." },
                new IngredientTypes { Id = 3, Name = "Meat", Description = "Animal flesh that is eaten as food." },
                new IngredientTypes { Id = 4, Name = "Dairy", Description = "Food produced from or containing the milk of mammals." },
                new IngredientTypes { Id = 5, Name = "Grain", Description = "Small, hard, dry seeds harvested for human or animal consumption." },
                new IngredientTypes { Id = 6, Name = "Seafood", Description = "Sea life regarded as food by humans, includes fish and shellfish." },
                new IngredientTypes { Id = 7, Name = "Spice", Description = "Substance used to flavor food, typically dried seeds, fruits, roots, or bark." },
                new IngredientTypes { Id = 8, Name = "Herb", Description = "Plants with savory or aromatic properties used for flavoring and garnishing food." },
                new IngredientTypes { Id = 9, Name = "Nuts & Seeds", Description = "Dry, edible fruits or seeds that usually have a high fat content." },
                new IngredientTypes { Id = 10, Name = "Beverage", Description = "Drinkable liquids other than water, may be hot or cold." }
            );
        }
    }

}
