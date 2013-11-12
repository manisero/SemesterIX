class OutputWriter:
    def __init__(self):
        pass

    @staticmethod
    def write_graph(root_node, file_name):
        if isinstance(file_name, str):
            with open(file_name, 'w') as output_file:
                OutputWriter.write_to_output_file(root_node, output_file)
        else:
            OutputWriter.write_to_output_file(root_node, file_name)

    @staticmethod
    def write_to_output_file(root_node, output_file):
        for node in root_node.iterator():
            output_file.write(str(node.node_id) + ' - ' + str(node.color) + '\n')
