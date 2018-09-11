using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System;


/* 
 * The "drag and drop" is not properly working. The DragDrop event returns an empty string.
*/
public class Main : MonoBehaviour {

    public InputField filePath;

    private Form f;
    private System.Windows.Forms.TextBox txtBox;
    private System.Windows.Forms.ComboBox drop;
    private System.Windows.Forms.ListBox listBox;

    private object[] dropDownItems = new object[] { "item1", "item2", "item3"};

    private List<string> listItem = new List<string>();

    // Use this for initialization
    void Start () {
        initializeWindowsFormComponent();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void initializeWindowsFormComponent()
    {
        f = new Form();
        f.Width = 1000;
        f.Height = 600;

        // button test
        System.Windows.Forms.Button btn = new System.Windows.Forms.Button();
        btn.Name = "btn";
        btn.Text = "open dialog";
        btn.Location = new Point(10, 50);
        btn.Width = 200;
        btn.Height = 30;
        btn.Click += open_dialog;
        f.Controls.Add(btn);

        System.Windows.Forms.Button closebtn = new System.Windows.Forms.Button();
        closebtn.Name = "closebtn";
        closebtn.Text = "close form";
        closebtn.Location = new Point(10, 100);
        closebtn.Width = 200;
        closebtn.Height = 30;
        closebtn.Click += close_form;
        f.Controls.Add(closebtn);

        // text test
        txtBox = new System.Windows.Forms.TextBox();
        txtBox.Name = "txtBox";
        txtBox.Text = "txtBox";
        txtBox.Location = new Point(10, 10);
        txtBox.Width = 200;
        txtBox.Height = 30;
        txtBox.AllowDrop = true;
        txtBox.Multiline = true;

        txtBox.DragDrop += Drag_Drop;
        txtBox.DragEnter += Drag_Enter;
        f.Controls.Add(txtBox);

        // drop down test
        drop = new System.Windows.Forms.ComboBox();
        drop.Name = "Drop";
        drop.Items.AddRange(dropDownItems);
        drop.Location = new Point(600, 10);
        drop.SelectedIndexChanged += dropDown_Selected;
        f.Controls.Add(drop);

        // create list item
        for (int i = 0; i < 10; i++)
        {
            listItem.Add("list:" + i.ToString());
        }

        // list test
        listBox = new System.Windows.Forms.ListBox();
        listBox.Name = "listBox";
        listBox.DataSource = listItem;
        listBox.Location = new Point(300, 10);
        listBox.SelectedIndexChanged += list_Selected;
        f.Controls.Add(listBox);

        f.TopMost = true;
    }

    public void OpenForm()
    {
        f.ShowDialog();
    }

    public void CloseForm()
    {
        f.Hide();
    }

    private void dropDown_Selected(object sender, System.EventArgs e)
    {
        txtBox.Text = dropDownItems[drop.SelectedIndex].ToString();
    }

    private void list_Selected(object sender, System.EventArgs e)
    {
        txtBox.Text = listItem[listBox.SelectedIndex].ToString();
    }

    private void open_dialog(object sender, System.EventArgs e)
    {
        Debug.Log(">>>>>> open_dialog");
        OpenFileDialog openfiledialog = new OpenFileDialog();
        openfiledialog.FileName = "openfiledialog";

        //open_file_dialog.Filter = "txtfile|*.txt";
        //open_file_dialog.CheckFileExists = true;

        openfiledialog.ShowDialog();

        filePath.text = openfiledialog.FileName;
    }

    private void close_form(object sender, System.EventArgs e)
    {
        CloseForm();
    }

    private void Drag_Drop(object sender, System.Windows.Forms.DragEventArgs e)
    {
        Debug.Log(">>>>>> Drag_Drop");
        string[] files = (string[])e.Data.GetData(System.Windows.Forms.DataFormats.FileDrop, false);
        Debug.Log(files.Length);
        txtBox.Text = files.Length.ToString();
        foreach (string file in files)
        {
            Debug.Log(file);
            txtBox.Text = file;
        }
    }

    private void Drag_Enter(object sender, System.Windows.Forms.DragEventArgs e)
    {
        Debug.Log(">>>>>> Drag_Enter");
        if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
        {
            e.Effect = System.Windows.Forms.DragDropEffects.Copy;
        }
    }
}
