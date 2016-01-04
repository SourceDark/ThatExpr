package com.ideasource.Util;

import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;
import java.util.Set;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Component;

import com.ideasource.Model.Expr;
import com.ideasource.Model.ExprRepository;

@Component
public class SearchUtil {

	@Autowired
	private static ExprRepository exprRepository;

	private static String cmpImageSoPath;

	@Value("${lcbq.cmpimageso.path}")
	public void setCmpImageSoPath(String path) {
		cmpImageSoPath = path;
	}

	private static String dstExpFolderPath;

	@Value("${lcbq.expr.path}")
	public void setDstExpFolderPath(String path) {
		dstExpFolderPath = path;
	}

//	static {
//		System.load(cmpImageSoPath);
//	}

	public static native float expWithExp(String srcExprPath, String dstExprPath);

	public static List<Expr> expWithFolder(String filename) throws Exception {
		String srcExpPath = dstExpFolderPath + "/" + filename;
		File root = new File(dstExpFolderPath);
		Set<File> imageFileSet = new HashSet<File>();
		HashMap<File, Float> resMap = new HashMap<File, Float>();

		getAllImageFiles(root, imageFileSet);
		for (File file : imageFileSet) {
			resMap.put(file, expWithExp(srcExpPath, file.getAbsolutePath()));
		}
		List<Map.Entry<File, Float>> infoIds = new ArrayList<Map.Entry<File, Float>>(resMap.entrySet());

		Collections.sort(infoIds, new Comparator<Map.Entry<File, Float>>() {
			public int compare(Map.Entry<File, Float> o1, Map.Entry<File, Float> o2) {
				if (o2.getValue() > o1.getValue())
					return 1;
				else
					return -1;
			}
		});

		List<Expr> res = new ArrayList<Expr>();
		for (int i = 0; i < infoIds.size(); i++) {
			Entry<File, Float> sortedRes = infoIds.get(i);
			String md5 = sortedRes.getKey().getName().split(".")[0];
			exprRepository.findAllByMd5(md5).get(0);
			if (sortedRes.getValue() > 0.5) {
				res.add(exprRepository.findAllByMd5(md5).get(0));
				System.out.println(sortedRes.getKey().getName() + ":" + sortedRes.getValue());
			}
		}

		return res;
	}

	final static void showAllFiles(File dir) throws Exception {
		File[] fs = dir.listFiles();
		for (int i = 0; i < fs.length; i++) {
			System.out.println(fs[i].getAbsolutePath());
			if (fs[i].isDirectory()) {
				try {
					showAllFiles(fs[i]);
				} catch (Exception e) {
				}
			}
		}
	}

	public static boolean isImageFile(File file) {
		String filename = file.getName();
		if (filename.endsWith("jpeg") || filename.endsWith("JPEG") || filename.endsWith("JPG")
				|| filename.endsWith("jpg") || filename.endsWith("png") || filename.endsWith("gif"))
			return true;
		return false;
	}

	final static void getAllImageFiles(File dir, Set<File> imageFileSet) throws Exception {
		File[] fs = dir.listFiles();
		for (int i = 0; i < fs.length; i++) {
			if (!fs[i].getName().startsWith(".") && !isSymlink(fs[i])) {
				if (fs[i].isDirectory()) {
					try {
						getAllImageFiles(fs[i], imageFileSet);
					} catch (Exception e) {
					}
				} else if (isImageFile(fs[i]))
					imageFileSet.add(fs[i]);
			}
		}
	}

	public static boolean isSymlink(File file) throws IOException {
		if (file == null)
			throw new NullPointerException("File must not be null");
		File canon;
		if (file.getParent() == null) {
			canon = file;
		} else {
			File canonDir = file.getParentFile().getCanonicalFile();
			canon = new File(canonDir, file.getName());
		}
		return !canon.getCanonicalFile().equals(canon.getAbsoluteFile());
	}

}
