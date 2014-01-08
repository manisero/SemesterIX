from dsa.dsa_key import DSAKey
from dsa_io.dsa_key_io import DSAKeyIO
from dsa_io.dsa_sign_io import DSASignIO


def main():
    while True:
        print '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~'
        print '1 Generate and export DSA key'
        print '2 Sign message'
        print '3 Verify signature\n'
        print '0 Quit application'
        print '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~'

        user_input = raw_input('Choose action: ')

        try:
            user_input_as_number = long(user_input)

            if user_input_as_number not in [0, 1, 2, 3]:
                raise ValueError('Invalid action')
        except:
            print '\nInvalid action chosen, please try again\n'
            continue

        if user_input_as_number == 0:
            break

        elif user_input_as_number == 1:
            private_key = raw_input('Pick private key file name: ')
            public_key = raw_input('Pick public key file name: ')

            export_key(private_key, public_key)

        elif user_input_as_number == 2:
            message = raw_input('Type message: ')
            dsa_key = raw_input('Enter private key file name: ')
            sign_file = raw_input('Pick sign file name: ')

            sign_message(message, dsa_key, sign_file)

        elif user_input_as_number == 3:
            dsa_key = raw_input('Enter public key file name: ')
            sign_file = raw_input('Enter sign file name: ')

            verify_signature(dsa_key, sign_file)


def export_key(private_key_filename, public_key_filename):
    dsa_key = DSAKey.generate_instance(2048)
    private_key = dsa_key.get_private_key()
    public_key = dsa_key.get_public_key()

    DSAKeyIO.export_key(private_key, private_key_filename)
    DSAKeyIO.export_key(public_key, public_key_filename)


def sign_message(message, dsa_key_filename, sign_filename):
    dsa_key = DSAKeyIO.import_key(dsa_key_filename)

    DSASignIO.export_sign_to_file(dsa_key, message, sign_filename)


def verify_signature(dsa_key_filename, sign_file):
    dsa_key = DSAKeyIO.import_key(dsa_key_filename)

    if DSASignIO.verify_sign_from_file(dsa_key, sign_file):
        print '\nSign is VALID!\n'
    else:
        print '\nSign is INVALID!\n'

if __name__ == '__main__':
    main()
